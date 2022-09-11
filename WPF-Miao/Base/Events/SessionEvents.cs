using System;
using System.Collections.Generic;

namespace Base.Events
{
    public class SessionEvents
    {
        #region Constructor

        public static SessionEvents Instance { get; private set; }

        static SessionEvents()
        {
            Instance = new SessionEvents();
        }

        private SessionEvents() { }

        #endregion Constructor

        #region Handler

        public event EventHandler<SesstionEventArgs> SessionEventHandler;

        #endregion Handler

        #region Subscribe

        public void Subscribe(EventHandler<SesstionEventArgs> handler)
        {
            SessionEventHandler += handler;
        }

        public void UnSubscribe(EventHandler<SesstionEventArgs> handler)
        {
            SessionEventHandler -= handler;
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
        public Dictionary<string, object> Session { get; private set; }

        public SesstionEventArgs(Dictionary<string, object> session)
        {
            Session = session;
        }
    }
}
