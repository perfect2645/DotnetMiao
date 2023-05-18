using System;
using System.Collections.Generic;

namespace Jian.appointment
{
    public class OrderEvent
    {
        #region Progress Event

        public event EventHandler<OrderEventArgs> ProgressEvent;

        public void Subscribe(EventHandler<OrderEventArgs> handler)
        {
            ProgressEvent += handler;
        }

        public void Publish(object? sender, OrderEventArgs e)
        {
            ProgressEvent?.Invoke(sender, e);
        }

        #endregion Progress Event

    }
    public class OrderEventArgs
    {
        public string OrderType { get; set; }
        public List<Order> OrderList { get; set; }
    }
}
