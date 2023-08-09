namespace MockU;

/// <summary>
///   A list of setups that have been configured on a mock,
///   in chronological order (that is, oldest setup first, most recent setup last).
/// </summary>
public interface ISetupList : IReadOnlyList<ISetup>
{
}
