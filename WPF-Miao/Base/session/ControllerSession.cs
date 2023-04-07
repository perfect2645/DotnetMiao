using HttpProcessor.Client;
using HttpProcessor.Container;
using System;
using System.Collections.Concurrent;
using Utils;

namespace Base.session
{
    public class ControllerSession<Client> : IControllerSession<Client> where Client : HttpClientBase
    {
        public ConcurrentDictionary<string, Client> ControllerCache { get; }

        public ControllerSession()
        {
            ControllerCache = new ConcurrentDictionary<string, Client>();
        }

        protected Client AddController(string key)
        {
            var controller = HttpServiceController.GetService<Client>();
            ControllerCache.AddOrUpdate(key, controller);

            return controller;
        }

        public Client GetController(string key)
        {
            try
            {
                if (!ControllerCache.ContainsKey(key))
                {
                    return AddController(key);
                }

                return ControllerCache[key];
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
                return null;
            }
        }
    }
}
