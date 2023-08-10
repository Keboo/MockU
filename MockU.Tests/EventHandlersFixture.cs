using Xunit;

namespace MockU.Tests;

public class EventHandlersFixture
{
    [Fact]
    public void Raising_event__directly_on_mock_object__does_not_trigger_handler__if_CallBase_false()
    {
        var handled = false;
        var mock = new Mock<HasEvent>();
        mock.Object.Event += () => handled = true;
        mock.Object.RaiseEvent();
        Assert.False(handled);
    }

    [Fact]
    public void Raising_event__directly_on_mock_object__triggers_handler__if_CallBase_true()
    {
        var handled = false;
        var mock = new Mock<HasEvent>() { CallBase = true };
        mock.Object.Event += () => handled = true;
        mock.Object.RaiseEvent();
        Assert.True(handled);
    }

    [Fact]
    public void Raising_event__directly_on_mock_object__does_not_trigger_handler__if_setup_without_CallBase_present()
    {
        var handled = false;
        var mock = new Mock<HasEvent>();
        mock.SetupAdd(m => m.Event += It.IsAny<Action>());
        mock.Object.Event += () => handled = true;
        mock.Object.RaiseEvent();
        Assert.False(handled);
    }

    [Fact]
    public void Raising_event__directly_on_mock_object__triggers_handler__if_setup_with_CallBase_present()
    {
        var handled = false;
        var mock = new Mock<HasEvent>();
        mock.SetupAdd(m => m.Event += It.IsAny<Action>()).CallBase();
        mock.Object.Event += () => handled = true;
        mock.Object.RaiseEvent();
        Assert.True(handled);
    }

    [Fact]
    public void Raising_event__using_Raise__triggers_handler__if_CallBase_false()
    {
        var handled = false;
        var mock = new Mock<HasEvent>();
        mock.Object.Event += () => handled = true;
        mock.Raise(m => m.Event += null);
        Assert.True(handled);
    }

    [Fact]
    public void Raising_event__using_Raise__does_not_trigger_handler__if_CallBase_true()
    {
        var handled = false;
        var mock = new Mock<HasEvent>() { CallBase = true };
        mock.Object.Event += () => handled = true;
        mock.Raise(m => m.Event += null);
        Assert.False(handled);
    }

    [Fact]
    public void Raising_event__using_Raise__triggers_handler__if_setup_without_CallBase_present()
    {
        var handled = false;
        var mock = new Mock<HasEvent>();
        mock.SetupAdd(m => m.Event += It.IsAny<Action>());
        mock.Object.Event += () => handled = true;
        mock.Raise(m => m.Event += null);
        Assert.True(handled);
    }

    [Fact]
    public void Raising_event__using_Raise__does_not_trigger_handler__if_setup_with_CallBase_present()
    {
        var handled = false;
        var mock = new Mock<HasEvent>();
        mock.SetupAdd(m => m.Event += It.IsAny<Action>()).CallBase();
        mock.Object.Event += () => handled = true;
        mock.Raise(m => m.Event += null);
        Assert.False(handled);
    }

    [Fact]
    public void Event_subscription__recorded__if_CallBase_false()
    {
        var mock = new Mock<HasEvent>();
        mock.Object.Event += () => { };
        Assert.Single(mock.Invocations);
    }

    [Fact]
    public void Event_subscription__recorded__if_CallBase_true()
    {
        var mock = new Mock<HasEvent>() { CallBase = true };
        mock.Object.Event += () => { };
        Assert.Single(mock.Invocations);
    }

    [Fact]
    public void Event_subscription__recorded__if_setup_present()
    {
        var mock = new Mock<HasEvent>();
        mock.SetupAdd(m => m.Event += It.IsAny<Action>());
        mock.Object.Event += () => { };
        Assert.Single(mock.Invocations);
    }

    [Fact]
    public void VerifyNoOtherCalls__sees_event_subscription__if_CallBase_false()
    {
        var mock = new Mock<HasEvent>();
        mock.Object.Event += () => { };
        Assert.Throws<MockException>(() => mock.VerifyNoOtherCalls());
    }

    [Fact]
    public void VerifyNoOtherCalls__sees_event_subscription__if_CallBase_true()
    {
        var mock = new Mock<HasEvent>() { CallBase = true };
        mock.Object.Event += () => { };
        Assert.Throws<MockException>(() => mock.VerifyNoOtherCalls());
    }

    [Fact]
    public void VerifyNoOtherCalls__sees_event_subscription__if_any_event_accessor_setup_present()
    {
        var mock = new Mock<HasEvent>() { CallBase = true };
        mock.SetupRemove(m => m.Event -= null);
        mock.Object.Event += () => { };
        Assert.Throws<MockException>(() => mock.VerifyNoOtherCalls());
    }

    public class HasEvent
    {
        public virtual event Action? Event;

        public void RaiseEvent() => Event?.Invoke();
    }

    [Fact]
    public async Task Can_raise_async_event_using_RaiseAsync()
    {
        var handled = false;
        var mock = new Mock<HasAsyncEvent>();
        mock.Object.Event += async () =>
        {
            await Task.Delay(50);
            handled = true;
        };
        await mock.RaiseAsync(m => m.Event += null);
        Assert.True(handled);
    }

    [Fact]
    public async Task Can_raise_parameterized_async_event_using_RaiseAsync()
    {
        var received = 0;
        var mock = new Mock<HasAsyncEvent>();
        mock.Object.ParameterizedEvent += async incoming =>
        {
            await Task.Delay(50);
            received = incoming;
        };
        await mock.RaiseAsync(m => m.ParameterizedEvent += null, 42);
        Assert.Equal(42, received);
    }

    public class HasAsyncEvent
    {
#pragma warning disable CA1070 // Do not declare event fields as virtual
#pragma warning disable CS0067 // The event 'EventHandlersFixture.HasAsyncEvent.Event' is never used
        public virtual event Func<Task>? Event;
        public virtual event Func<int, Task>? ParameterizedEvent;
#pragma warning restore CS0067 // The event 'EventHandlersFixture.HasAsyncEvent.Event' is never used
#pragma warning restore CA1070 // Do not declare event fields as virtual
    }
}
