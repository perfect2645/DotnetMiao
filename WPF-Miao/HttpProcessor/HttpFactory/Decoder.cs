using System.Web;

namespace HttpProcessor.HttpFactory
{
    public static class Decoder
    {
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        public static string DecodeUrl(this string str)
        {
            return UrlDecode(str);
        }
    }
}
