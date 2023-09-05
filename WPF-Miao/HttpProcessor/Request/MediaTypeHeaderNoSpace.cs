using System.Net.Http.Headers;

namespace HttpProcessor.Request
{
    public class MediaTypeHeaderNoSpace : MediaTypeHeaderValue
    {
        public MediaTypeHeaderNoSpace(string mediaType) : base(mediaType)
        {
        }
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(base.CharSet))
            {
                return base.MediaType + ";charset=utf-8";
            }
            return base.MediaType + ";charset=" + base.CharSet;
        }
    }
}
