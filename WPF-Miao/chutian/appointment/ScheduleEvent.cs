using System;
using System.Collections.Generic;

namespace chutian.appointment
{
    public class ScheduleEvent
    {
        #region Progress Event

        public event EventHandler<ScheduleEventArgs> ProgressEvent;

        public void Subscribe(EventHandler<ScheduleEventArgs> handler)
        {
            ProgressEvent += handler;
        }

        public void Publish(object? sender, ScheduleEventArgs e)
        {
            ProgressEvent?.Invoke(sender, e);
        }

        #endregion Progress Event

    }
    public class ScheduleEventArgs
    {
        public string OrderType { get; set; }
        public List<Order> OrderList { get; set; }
    }
}
