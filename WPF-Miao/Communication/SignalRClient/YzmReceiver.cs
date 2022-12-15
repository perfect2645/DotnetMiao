using Microsoft.AspNetCore.SignalR.Client;

namespace Communication.SignalRClient
{
    public class YzmReceiver
    {
        private readonly Action<string, string> _receiveYzmAction;

        public YzmReceiver(Action<string, string> receiveYzmAction)
        {
            _receiveYzmAction = receiveYzmAction;
            CreateConnection();
        }

        private async void CreateConnection()
        {
            var connect = new ConnectParameter();
            connect.WindowName = "JKZL";
            var connection = new HubConnectionBuilder()
           .WithUrl("http://114.117.245.177:5200/myTestHub")
           .Build();

            connection.On<string, string>("SendMsgClient", _receiveYzmAction);

            await connection.StartAsync();

            var s1 = connection.State;
        }
    }
}
