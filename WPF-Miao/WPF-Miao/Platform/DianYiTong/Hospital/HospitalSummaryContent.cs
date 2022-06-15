using HttpProcessor.Client;

namespace WPF_Miao.Platform.DianYiTong.Hospital
{
    internal class HospitalSummaryContent : HttpClientContentBase
    {
        public HospitalSummaryContent(string requestUrl) : base(requestUrl)
        {
        }
    }
}
