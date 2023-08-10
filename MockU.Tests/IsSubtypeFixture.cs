using Xunit;

namespace MockU.Tests;

public class IsSubtypeFixture
{
    [Fact]
    public void It_IsSubtype_works_for_base_type_relationships()
    {
        var mock = new Mock<IX>();

        mock.Object.Method<ArgumentOutOfRangeException>(); // should match
        mock.Object.Method<Exception>();
        mock.Object.Method<ArgumentException>(); // should match
        mock.Object.Method<string>();
        mock.Object.Method<ArgumentNullException>(); // should match
        mock.Object.Method<InvalidOperationException>();
        mock.Object.Method<bool>();

        mock.Verify(m => m.Method<It.IsSubtype<ArgumentException>>(), Times.Exactly(3));
    }

    [Fact]
    public void It_IsSubtype_works_for_interface_implementation_relationships()
    {
        var mock = new Mock<IX>();

        mock.Object.Method<Stream>(); // should match
        mock.Object.Method<string>();
        mock.Object.Method<IEnumerator<int>>(); // should match
        mock.Object.Method<Exception>();
        mock.Object.Method<bool>();

        mock.Verify(m => m.Method<It.IsSubtype<IDisposable>>(), Times.Exactly(2));
    }

    public interface IX
    {
        void Method<T>();
    }
}
