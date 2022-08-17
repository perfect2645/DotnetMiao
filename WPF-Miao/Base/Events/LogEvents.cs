using System;
using System.Collections.Generic;

namespace Base.Events
{
    public class LogEvents
    {
        public event EventHandler<LogEventArgs> PrintLogEvent;

        public void Subscribe(EventHandler<LogEventArgs> handler)
        {
            PrintLogEvent += handler;
        }

        public void Publish(object? sender, LogEventArgs e)
        {
            PrintLogEvent?.Invoke(sender, e);
        }

        public void Publish(object? sender, Dictionary<string, object> args, string message = null)
        {
            var e = new LogEventArgs
            {
                Items = args,
                Message = message ?? sender.GetType().Name,
            };
            PrintLogEvent?.Invoke(sender, e);
        }
    }

    public class LogEventArgs
    {
        public string Message { get; set; }
        public Dictionary<string, object> Items { get; set; }
    }
}
