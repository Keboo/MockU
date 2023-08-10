using System.Reflection;

using Xunit;

namespace MockU.Tests;

/// <summary>
/// Tests for the <see cref="DefaultValueProvider"/> abstract base class.
/// </summary>
public class DefaultValueProviderFixture

/* Unmerged change from project 'Moq.Tests(net6.0)'
Before:
        private static MethodInfo fooActionMethod = typeof(IFoo).GetMethod(nameof(IFoo.Action));
        private static ParameterInfo fooActionMethodParameter = typeof(IFoo).GetMethod(nameof(IFoo.Action)).GetParameters()[0];
        private static MethodInfo fooFuncMethod = typeof(IFoo).GetMethod(nameof(IFoo.Func));
After:
        static MethodInfo fooActionMethod = typeof(IFoo).GetMethod(nameof(IFoo.Action));
        static ParameterInfo fooActionMethodParameter = typeof(IFoo).GetMethod(nameof(IFoo.Action)).GetParameters()[0];
        static MethodInfo fooFuncMethod = typeof(IFoo).GetMethod(nameof(IFoo.Func));
*/
{
    static MethodInfo fooActionMethod = typeof(IFoo).GetMethod(nameof(IFoo.Action));
    static ParameterInfo fooActionMethodParameter = typeof(IFoo).GetMethod(nameof(IFoo.Action)).GetParameters()[0];
    static MethodInfo fooFuncMethod = typeof(IFoo).GetMethod(nameof(IFoo.Func));


    /* Unmerged change from project 'Moq.Tests(net6.0)'
    Before:
            private DefaultValueProvider defaultValueProvider;
            private Mock<IFoo> fooMock;
    After:
            DefaultValueProvider defaultValueProvider;
            Mock<IFoo> fooMock;
    */
    DefaultValueProvider defaultValueProvider;
    Mock<IFoo> fooMock;

    public DefaultValueProviderFixture()
    {
        defaultValueProvider = new DefaultValueProviderStub();
        fooMock = new Mock<IFoo>();
    }

    [Fact]
    public void GetDefaultParameterValue_returns_same_value_as_GetDefaultValue_if_GetDefaultParameterValue_not_overridden()
    {
        var _ = fooMock;
        var parameter = fooActionMethodParameter;
        var expected = defaultValueProvider.GetDefaultValue(parameter.ParameterType, _);

        var actual = defaultValueProvider.GetDefaultParameterValue(parameter, _);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetDefaultReturnValue_returns_same_value_as_GetDefaultValue_if_GetDefaultReturnValue_not_overridden()
    {
        var _ = fooMock;
        var method = fooFuncMethod;
        var expected = defaultValueProvider.GetDefaultValue(method.ReturnType, _);

        var actual = defaultValueProvider.GetDefaultReturnValue(method, _);

        Assert.Equal(expected, actual);
    }

    public interface IFoo
    {
        void Action(object arg);
        object Func();

        
    }

    sealed class DefaultValueProviderStub : DefaultValueProvider
    {
        protected internal override object GetDefaultValue(Type type, Mock mock)
        {
            return 42;
        }
    }
}
