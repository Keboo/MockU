using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace MockU;

internal sealed class StubbedPropertySetup : Setup
{
    private object? value;

    public StubbedPropertySetup(Mock mock, LambdaExpression expression, MethodInfo getter, MethodInfo setter, object? initialValue)
        : base(originalExpression: null, mock, new PropertyAccessorExpectation(expression, getter, setter))
    {
        // NOTE:
        //
        // At this time, this setup type does not require both a `getter` and a `setter` to be present,
        // even though a stubbed property doesn't make much sense if either one is missing.
        //
        // This supports the `HandleAutoSetupProperties` interception step backing `SetupAllProperties`.
        // Once there is another dedicated setup type for `SetupAllProperties`, we may want to require
        // both accessors here.

        value = initialValue;

        MarkAsVerifiable();
    }

    public override IEnumerable<Mock> InnerMocks
    {
        get
        {
            var innerMock = TryGetInnerMockFrom(value);
            if (innerMock != null)
            {
                yield return innerMock;
            }
        }
    }

    protected override void ExecuteCore(Invocation invocation)
    {
        if (invocation.Method.ReturnType == typeof(void))
        {
            Debug.Assert(invocation.Method.IsSetAccessor());
            Debug.Assert(invocation.Arguments.Length == 1);

            value = invocation.Arguments[0];
        }
        else
        {
            Debug.Assert(invocation.Method.IsGetAccessor());

            invocation.ReturnValue = value;
        }
    }

    public override string ToString()
    {
        return base.ToString() + " (stubbed)";
    }

    protected override void VerifySelf()

    

    

    
    {
    }

    private sealed class PropertyAccessorExpectation : Expectation
    {
        private readonly LambdaExpression expression;
        private readonly MethodInfo getter;
        private readonly MethodInfo setter;

        public PropertyAccessorExpectation(LambdaExpression expression, MethodInfo getter, MethodInfo setter)
        {
            Debug.Assert(expression != null);
            Debug.Assert(expression.IsProperty());
            Debug.Assert(getter != null || setter != null);

            this.expression = expression;
            this.getter = getter;
            this.setter = setter;
        }

        public override LambdaExpression Expression => expression;

        public override bool Equals(Expectation obj)
        {
            return obj is PropertyAccessorExpectation other
                && other.getter == getter
                && other.setter == setter;
        }

        public override int GetHashCode()
        {
            return unchecked((getter?.GetHashCode() ?? 0) + 103 * (setter?.GetHashCode() ?? 0));
        }

        public override bool IsMatch(Invocation invocation)
        {
            var methodName = invocation.Method.Name;
            return methodName == getter.Name || methodName == setter.Name;
        }
    }
}
