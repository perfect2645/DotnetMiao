using System;
using System.Collections.Generic;

namespace Jkchegu.appointment
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
        public List<Order> OrderList { get; set; }
    }
}
