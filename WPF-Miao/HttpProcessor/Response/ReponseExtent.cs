using Utils;

namespace HttpProcessor.Response
{
    public static class ReponseExtent
    {
        public static byte[] GetImageContent(this Dictionary<string, object> source)
        {
            if (!source.HasItem()) 
            {
                return new byte[0];
            }

            if (!source.ContainsKey("imageContent"))
            {
                return new byte[0];
            }

            var imgContent = source["imageContent"] as byte[];
            if (!imgContent.HasItem())
            {
                return new byte[0];
            }

            return imgContent;
        }
    }
}
