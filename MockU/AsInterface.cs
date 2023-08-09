

namespace MockU;

internal class AsInterface<TInterface> : Mock<TInterface>
    where TInterface : class

    

    

    
{
    private Mock owner;

    public AsInterface(Mock owner)
        : base(true)
    {
        this.owner = owner;
    }

    internal override List<Type> AdditionalInterfaces => owner.AdditionalInterfaces;

    internal override Dictionary<Type, object> ConfiguredDefaultValues => owner.ConfiguredDefaultValues;

    internal override object[] ConstructorArguments => owner.ConstructorArguments;

    internal override InvocationCollection MutableInvocations => owner.MutableInvocations;

    internal override bool IsObjectInitialized => owner.IsObjectInitialized;

    internal override Type MockedType => owner.MockedType;

    public override MockBehavior Behavior => owner.Behavior;

    public override bool CallBase
    {
        get { return owner.CallBase; }
        set { owner.CallBase = value; }
    }

    public override DefaultValueProvider DefaultValueProvider
    {
        get => owner.DefaultValueProvider;
        set => owner.DefaultValueProvider = value;
    }

    internal override EventHandlerCollection EventHandlers => owner.EventHandlers;

    internal override Type[] InheritedInterfaces => owner.InheritedInterfaces;

    public override TInterface Object
    {
        get { return owner.Object as TInterface; }
    }

    internal override SetupCollection MutableSetups => owner.MutableSetups;

    public override Switches Switches
    {
        get => owner.Switches;
        set => owner.Switches = value;
    }

    public override Mock<TNewInterface> As<TNewInterface>()
    {
        return owner.As<TNewInterface>();
    }

    protected override object OnGetObject()
    {
        return owner.Object;
    }

    public override string ToString()
    {
        return owner.ToString();
    }
}
