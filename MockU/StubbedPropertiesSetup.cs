

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;

using E = System.Linq.Expressions.Expression;

namespace MockU;

internal sealed class StubbedPropertiesSetup : Setup
{
    private readonly ConcurrentDictionary<string, object> values;

    public StubbedPropertiesSetup(Mock mock, DefaultValueProvider? defaultValueProvider = null)
        : base(originalExpression: null, mock, new PropertyAccessorExpectation(mock))
    {
        values = new ConcurrentDictionary<string, object>();
        DefaultValueProvider = defaultValueProvider ?? mock.DefaultValueProvider;

        MarkAsVerifiable();
    }

    public DefaultValueProvider DefaultValueProvider { get; }

    public override IEnumerable<Mock> InnerMocks
    {
        get
        {
            foreach (var value in values.Values)
            {
                var innerMock = TryGetInnerMockFrom(value);
                if (innerMock != null)
                {
                    yield return innerMock;
                }
            }
        }
    }

    public void SetProperty(string propertyName, object value)
    {
        values[propertyName] = value;
    }

    protected override void ExecuteCore(Invocation invocation)
    {
        if (invocation.Method.ReturnType == typeof(void))
        {
            Debug.Assert(invocation.Method.IsSetAccessor());
            Debug.Assert(invocation.Arguments.Length == 1);

            var propertyName = invocation.Method.Name.Substring(4);
            values[propertyName] = invocation.Arguments[0];
        }
        else
        {
            Debug.Assert(invocation.Method.IsGetAccessor());

            var propertyName = invocation.Method.Name.Substring(4);
            var value = values.GetOrAdd(propertyName, pn => Mock.GetDefaultValue(invocation.Method, out _, DefaultValueProvider));
            invocation.ReturnValue = value;
        }
    }

    protected override void VerifySelf()
    {
    }

    private sealed class PropertyAccessorExpectation : Expectation
    {
        private readonly LambdaExpression expression;

        public PropertyAccessorExpectation(Mock mock)
        {
            Debug.Assert(mock != null);

            var mockType = mock.GetType();
            var setupAllPropertiesMethod = mockType.GetMethod(nameof(Mock<object>.SetupAllProperties));
            var mockedType = setupAllPropertiesMethod.ReturnType.GetGenericArguments()[0];
            var mockGetMethod = Mock.GetMethod.MakeGenericMethod(mockedType);
            var mockParam = E.Parameter(mockedType, "m");
            expression = E.Lambda(E.Call(E.Call(mockGetMethod, mockParam), setupAllPropertiesMethod), mockParam);
        }

        public override LambdaExpression Expression => expression;

        public override bool Equals(Expectation other)
        {
            return other is PropertyAccessorExpectation pae && ExpressionComparer.Default.Equals(expression, pae.expression);
        }

        public override int GetHashCode()
        {
            return typeof(PropertyAccessorExpectation).GetHashCode();
        }

        public override bool IsMatch(Invocation invocation)
        {
            return invocation.Method.IsPropertyAccessor();
        }
    }
}
