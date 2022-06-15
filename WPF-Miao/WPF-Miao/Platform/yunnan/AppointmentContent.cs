using HttpProcessor.Client;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentContent : HttpClientContentBase
    {
        public AppointmentContent(string requestUrl) : base(requestUrl)
        {
        }
    }
}
