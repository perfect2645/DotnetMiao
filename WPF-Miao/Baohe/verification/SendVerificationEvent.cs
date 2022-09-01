using System;

namespace Baohe.verification
{
    public class SendVerificationEvent
    {
        internal static event EventHandler<VerifyArgs> VerifyEvent;

        internal static void Subscribe(EventHandler<VerifyArgs> handler)
        {
            VerifyEvent += handler;
        }

        internal static void Publish(object? sender, VerifyArgs e)
        {
            VerifyEvent?.Invoke(sender, e);
        }
    }

    internal class VerifyArgs : EventArgs
    {
        public string OrderKey { get; set; }
        public string PhoneNo { get; set; }

        public VerifyArgs(string orderKey, string phoneNo)
        {
            OrderKey = orderKey;
            PhoneNo = phoneNo;
        }
    }
}
