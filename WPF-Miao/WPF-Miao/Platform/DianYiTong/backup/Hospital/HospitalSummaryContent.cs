using HttpProcessor.Client;

namespace WPF_Miao.Platform.DianYiTong.Hospital
{
    internal class HospitalSummaryContent : HttpMessageContentBase
    {
        public HospitalSummaryContent(string requestUrl) : base(requestUrl)
        {
        }
    }
}
