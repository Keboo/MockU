using System.ComponentModel;

namespace MockU;

/// <summary>
/// Helper class to setup a full trace between many mocks
/// </summary>
public class MockSequence
{
    private int sequenceStep;
    private int sequenceLength;

    /// <summary>
    /// Initialize a trace setup
    /// </summary>
    public MockSequence()
    {
        sequenceLength = 0;
        sequenceStep = 0;
    }

    /// <summary>
    /// Allow sequence to be repeated
    /// </summary>
    public bool Cyclic { get; set; }

    

    

    
    private void NextStep()
    {
        sequenceStep++;
        if (Cyclic)
            sequenceStep = sequenceStep % sequenceLength;
    }

    internal ISetupConditionResult<TMock> For<TMock>(Mock<TMock> mock)
        where TMock : class
    {
        var expectationPosition = sequenceLength++;

        return new WhenPhrase<TMock>(mock, new Condition(
            condition: () => expectationPosition == sequenceStep,
            success: NextStep));
    }
}

/// <summary>
/// Contains extension methods that are related to <see cref="MockSequence"/>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class MockSequenceHelper
{
    /// <summary>
    /// Perform an expectation in the trace.
    /// </summary>
    public static ISetupConditionResult<TMock> InSequence<TMock>(
        this Mock<TMock> mock,
        MockSequence sequence)
        where TMock : class
    {
        Guard.NotNull(sequence, nameof(sequence));

        return sequence.For(mock);
    }
}
