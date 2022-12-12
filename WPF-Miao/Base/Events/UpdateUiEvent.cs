using System;

namespace Base.Events
{
    public class UpdateUiEvent
    {
        #region Progress Event

        public event EventHandler<UiEventArgs> UiEvent;

        public void Subscribe(EventHandler<UiEventArgs> handler)
        {
            UiEvent += handler;
        }

        public void Publish(object? sender, UiEventArgs e)
        {
            UiEvent?.Invoke(sender, e);
        }

        #endregion Progress Event

    }
    public class UiEventArgs
    {
        public string Field { get; set; }
        public object Value { get; set; }
    }
}
