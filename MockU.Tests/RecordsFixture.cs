

using Xunit;

namespace MockU.Tests;

public class RecordsFixture
{
    [Fact]
    public void Can_mock_EmptyRecord()
    {
        _ = new Mock<EmptyRecord>().Object;
    }

    [Fact]
    public void Can_mock_DerivedEmptyRecord()
    {
        _ = new Mock<DerivedEmptyRecord>().Object;
    }

    public record EmptyRecord
    {
    }

    public record DerivedEmptyRecord : EmptyRecord
    {
    }
}
