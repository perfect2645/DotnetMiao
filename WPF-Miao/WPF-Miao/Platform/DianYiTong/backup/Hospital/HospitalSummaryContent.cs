using HttpProcessor.Content;

namespace WPF_Miao.Platform.DianYiTong.Hospital
{
    internal class HospitalSummaryContent : HttpMessageContent
    {
        public HospitalSummaryContent(string requestUrl) : base(requestUrl)
        {
        }
    }
}
