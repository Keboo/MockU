using System.Linq.Expressions;

using Xunit;

namespace MockU.Tests.Matchers;

public class AnyMatcherFixture
{
    [Fact]
    public void MatchesNull()
    {
        var expr = ToExpression(() => It.IsAny<object>()).Body;

        var (matcher, _) = MatcherFactory.CreateMatcher(expr);

        Assert.True(matcher.Matches(null, typeof(object)));
    }

    [Fact]
    public void MatchesIfAssignableType()
    {
        var expr = ToExpression(() => It.IsAny<object>()).Body;

        var (matcher, _) = MatcherFactory.CreateMatcher(expr);

        Assert.True(matcher.Matches("foo", typeof(object)));
    }

    [Fact]
    public void MatchesIfAssignableInterface()
    {
        var expr = ToExpression(() => It.IsAny<IDisposable>()).Body;

        var (matcher, _) = MatcherFactory.CreateMatcher(expr);

        Assert.True(matcher.Matches(new Disposable(), typeof(IDisposable)));
    }

    [Fact]
    public void DoesNotMatchIfNotAssignableType()
    {
        var expr = ToExpression(() => It.IsAny<IFormatProvider>()).Body;

        var (matcher, _) = MatcherFactory.CreateMatcher(expr);

        Assert.False(matcher.Matches("foo", typeof(IFormatProvider)));
    }

    static LambdaExpression ToExpression<TResult>(Expression<Func<TResult>> expr)
    {
        return expr;
    }

    class Disposable : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
