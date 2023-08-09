namespace MockU;

[Flags]
internal enum MockExceptionReasons
{
    IncorrectNumberOfCalls = 1,
    NoMatchingCalls = 4,
    NoSetup = 8,
    ReturnValueRequired = 16,
    UnmatchedSetup = 32,
    UnverifiedInvocations = 64,
}
