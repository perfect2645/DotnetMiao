using HttpProcessor.Client;
using System.Collections.Concurrent;

namespace Base.session
{
    public interface IControllerSession<Client> where Client : HttpClientBase
    {
        ConcurrentDictionary<string, Client> ControllerCache { get; }
        Client GetController(string key);
    }
}
