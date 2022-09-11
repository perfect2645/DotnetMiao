using System;

namespace Baohe.appointment
{
    public enum OrderStatus
    {
        Pending,
        Running,
        Failed,
        Cancelled,
    }

    public class OrderStatusEvent
    {
        internal static event EventHandler<OrderStatusArgs> OrderStatusChangedEvent;

        internal void Subscribe(EventHandler<OrderStatusArgs> handler)
        {
            OrderStatusChangedEvent += handler;
        }

        internal void Publish(object? sender, OrderStatusArgs e)
        {
            OrderStatusChangedEvent?.Invoke(sender, e);
        }
    }

    public class OrderStatusArgs : EventArgs
    {
        public string OrderKey { get; set; }
        public OrderStatus Status { get; set; }

        public OrderStatusArgs(string orderKey, OrderStatus status)
        {
            OrderKey = orderKey;
            Status = status;
        }
    }
}
