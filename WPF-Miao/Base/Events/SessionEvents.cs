using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Events
{
    public class SessionEvents
    {
        #region Handler

        public event EventHandler<SesstionEventArgs> SessionEventHandler;

        #endregion Handler

        #region Subscribe

        public void Subscribe(EventHandler<SesstionEventArgs> handler)
        {
            SessionEventHandler += handler;
        }

        #endregion Subscribe

        #region Publish

        public void Publish(object? sender, SesstionEventArgs args)
        {
            SessionEventHandler?.Invoke(sender, args);
        }

        #endregion Publish
    }

    public class SesstionEventArgs : EventArgs
    {
        public Dictionary<string, object> Session { get; set; }
    }
}
