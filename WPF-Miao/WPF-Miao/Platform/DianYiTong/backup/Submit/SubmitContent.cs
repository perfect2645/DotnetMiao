using HttpProcessor.Content;

namespace WPF_Miao.Platform.DianYiTong.Submit
{
    internal class SubmitContent : HttpMessageContent
    {
        public SubmitContent(string requestUrl) : base(requestUrl)
        {
        }
    }
}
