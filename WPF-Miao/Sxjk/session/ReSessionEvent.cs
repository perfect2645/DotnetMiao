using Sxjk.appointment;
using System;
using System.Collections.Generic;

namespace Sxjk.session
{
    public class ReSessionEvent
    {
        #region Progress Event

        public bool IsReSession = false;
        public readonly object _resessionLock = new object();

        public event EventHandler<ResessionEventArgs> ReloginEvent;

        public void Subscribe(EventHandler<ResessionEventArgs> handler)
        {
            ReloginEvent += handler;
        }

        public void Publish(object? sender, ResessionEventArgs e)
        {
            lock(_resessionLock)
            {
                if (!IsReSession)
                {
                    IsReSession = true;
                    lock (_resessionLock)
                    {
                        ReloginEvent?.Invoke(sender, e);
                    }
                }
            }
        }

        #endregion Progress Event

    }
    public class ResessionEventArgs
    {
        public string Message { get; set; }
    }
}
