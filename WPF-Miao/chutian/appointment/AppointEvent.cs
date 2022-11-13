using System;
using System.Collections.Generic;

namespace chutian.appointment
{
    public class AppointEvent
    {
        #region Progress Event

        public event EventHandler<AppointEventArgs> ProgressEvent;

        public void Subscribe(EventHandler<AppointEventArgs> handler)
        {
            ProgressEvent += handler;
        }

        public void Publish(object? sender, AppointEventArgs e)
        {
            ProgressEvent?.Invoke(sender, e);
        }

        #endregion Progress Event

    }
    public class AppointEventArgs
    {
        public string OrderType { get; set; }
        public List<Order> OrderList { get; set; }
    }
}
