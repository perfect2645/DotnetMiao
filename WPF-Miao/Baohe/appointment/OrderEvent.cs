using System;
using System.Net.Http;

namespace Baohe.appointment
{
    public class OrderEvent
    {
        internal static event EventHandler<OrderArgs> AppointEvent;

        internal static void Subscribe(EventHandler<OrderArgs> handler)
        {
            AppointEvent += handler;
        }

        internal static void Publish(object? sender, OrderArgs e)
        {
            AppointEvent?.Invoke(sender, e);
        }
    }

    internal class OrderArgs : EventArgs
    {
        public string OrderKey { get; set; }
        public AppointmentContent Content { get; set; }

        public OrderArgs(string orderKey, AppointmentContent content)
        {
            OrderKey = orderKey;
            Content = content;
        }
    }
}
