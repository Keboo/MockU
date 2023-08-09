namespace MockU.Language.Flow;

internal class SetterSetupPhrase<T, TProperty> : VoidSetupPhrase<T>, ISetupSetter<T, TProperty> where T : class
{
    public SetterSetupPhrase(MethodCall setup) : base(setup)
    {
    }

    public ICallbackResult Callback(Action<TProperty> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }
}
