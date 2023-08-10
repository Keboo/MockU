
namespace MockU.Tests.Async;

using System.Threading.Tasks;

using Xunit;

public class AwaitableFixture
{
    [Fact]
    public void TryGetResultRecursive_is_recursive()
    {
        const int expectedResult = 42;
        var obj = Task.FromResult(Task.FromResult(expectedResult));
        var result = Awaitable.TryGetResultRecursive(obj);
        Assert.Equal(expectedResult, result);
    }
}
