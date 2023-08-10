

using Xunit;

namespace MockU.Tests;

public class HidePropertyFixture
{
    public class A
    {
        public string Prop { get; }
    }

    public class B : A
    {
        public new virtual int Prop { get; }
    }

    public class C : B
    {
    }

    [Fact]
    public void SetupsDerivedProperty()
    {
        var mock = new Mock<C>();
        var value = 5;

        mock.Setup(m => m.Prop).Returns(value);

        Assert.Equal(value, mock.Object.Prop);
    }
}
