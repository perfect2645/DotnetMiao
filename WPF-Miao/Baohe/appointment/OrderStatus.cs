using System;

namespace Baohe.appointment
{
    internal enum OrderStatus
    {
        Pending,
        Running,
        Failed,
        Cancelled,
    }

    internal class OrderStatusEvent
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

    internal class OrderStatusArgs : EventArgs
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
