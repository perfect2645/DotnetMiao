using System;
using System.Collections.Generic;
using System.Threading;
using Utils.datetime;

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

        public void Publish(object? sender, string message)
        {
            var e = new LogEventArgs
            {
                Items = new Dictionary<string, object>(),
                Message = $"[{Thread.CurrentThread.ManagedThreadId}]{message ?? sender.GetType().Name} - Time={DateTimeUtil.GetNow()}",
            };
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

        public void Publish(object? sender, List<Dictionary<string, object>> args, string message = null)
        {
            foreach(var arg in args)
            {
                var e = new LogEventArgs
                {
                    Items = arg,
                    Message = message ?? sender.GetType().Name,
                };
                PrintLogEvent?.Invoke(sender, e);
            }
        }
    }

    public class LogEventArgs
    {
        public LogEventArgs()
        {
        }
        public LogEventArgs(string log)
        {
            Message = log;
        }

        public string Message { get; set; }
        public Dictionary<string, object> Items { get; set; }
    }
}
