using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MockU;

/// <summary>
///   A per-thread observer that records invocations to matchers for later inspection.
/// </summary>
/// <remarks>
///   <para>
///     This component requires the active cooperation of the respective subsystem.
///     That is, invoked matchers call into <see cref="OnMatch(Match)"/> if an
///     observer is active on the current thread.
///   </para>
/// </remarks>
internal sealed class MatcherObserver : IDisposable
{
    [ThreadStatic]
    private static Stack<MatcherObserver>? activations;

    public static MatcherObserver Activate()
    {
        var activation = new MatcherObserver();

        var activations = MatcherObserver.activations;
        if (activations == null)
        {
            MatcherObserver.activations = activations = new Stack<MatcherObserver>();
        }
        activations.Push(activation);

        return activation;
    }

    public static bool IsActive(out MatcherObserver? observer)
    {
        var activations = MatcherObserver.activations;

        if (activations?.Count > 0)
        {
            observer = activations.Peek();
            return true;
        }
        else
        {
            observer = null;
            return false;
        }
    }

    private int timestamp;
    private List<Observation>? observations;
    
    private MatcherObserver()
    {
    }

    public void Dispose()
    {
        var activations = MatcherObserver.activations;
        Debug.Assert(activations != null && activations.Count > 0);
        activations.Pop();
    }

    /// <summary>
    ///   Returns the current timestamp. The next call will return a timestamp greater than this one,
    ///   allowing you to order invocations and matcher observations.
    /// </summary>
    public int GetNextTimestamp()
    {
        return ++timestamp;
    }

    /// <summary>
    ///   Adds the specified <see cref="Match"/> as an observation.
    /// </summary>
    public void OnMatch(Match match)
    {
        observations ??= new List<Observation>();

        observations.Add(new Observation(GetNextTimestamp(), match));
    }

    /// <summary>
    ///   Checks whether at least one <see cref="Match"/> observation is available,
    ///   and if so, returns the last one.
    /// </summary>
    /// <param name="match">The observed <see cref="Match"/> matcher observed last.</param>
    public bool TryGetLastMatch([NotNullWhen(true)] out Match? match)
    {
        if (observations != null && observations.Count > 0)
        {
            match = observations.Last().Match;
            return true;
        }

        match = default;
        return false;
    }

    public IEnumerable<Match> GetMatchesBetween(int fromTimestampInclusive, int toTimestampExclusive)
    {
        if (observations != null)
        {
            return observations
                       .Where(o => fromTimestampInclusive <= o.Timestamp && o.Timestamp < toTimestampExclusive)
                       .Select(o => o.Match);
        }
        else
        {
            return Enumerable.Empty<Match>();

            

            

            
        }
    }

    private readonly struct Observation
    {
        public readonly int Timestamp;
        public readonly Match Match;

        public Observation(int timestamp, Match match)
        {
            Timestamp = timestamp;
            Match = match;
        }
    }
}
