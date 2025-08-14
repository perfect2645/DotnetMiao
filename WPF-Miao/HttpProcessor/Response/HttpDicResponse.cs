using HttpProcessor.Response;
using Logging;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text.Json;
using Utils;

namespace HttpProcessor.Client
{
    public class HttpDicResponse : HttpResponseBase
    {
        #region Properties

        public ushort StatusCode { get; private set; }

        public string Message { get; private set; }

        public string ContentStr { get; private set; }

        public Dictionary<string, object> Body { get; private set; }

        public JsonDocument JsonBody { get; private set; }

        public Func<string, string> DecodeAction { get; set; }

        #endregion Properties

        #region Constructor

        public HttpDicResponse(string error)
        {
            Message = error;
        }

        public HttpDicResponse(HttpResponseMessage response, Func<string, string> decodeAction)
        {
            DecodeAction = decodeAction;
            try
            {
                BuildHeaders(response.Headers);
                BuildBody(response.Content);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
            }
        }

        public HttpDicResponse(HttpResponseMessage response)
        {
            try
            {
                BuildHeaders(response.Headers);
                BuildBody(response.Content);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
            }

        }

        #endregion Constructor

        #region Body

        protected override void BuildBody(HttpContent content)
        {
            var contentStr = string.Empty;
            var mediaType = content.Headers?.ContentType?.MediaType;
            if (mediaType!= null && mediaType.Contains("image"))
            {
                BuildImageBody(content, mediaType);
                return;
            }
            else if (content.Headers?.ContentEncoding?.Contains("gzip") == true)
            {
                contentStr = BuildBodyFromGzip(content);
            }
            else
            {
                contentStr = content.ReadAsStringAsync().Result;
            }

            contentStr = DecodeAction?.Invoke(contentStr) ?? contentStr;

            ContentStr = contentStr;
            JsonBody = JsonDocument.Parse(contentStr);

            try
            {
                if (JsonBody.RootElement.ValueKind == JsonValueKind.Array)
                {
                    var arrayContent = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(contentStr);
                    Body = new Dictionary<string, object>
                    {
                        {"Array", arrayContent},
                    };
                    return;
                }
                var contentDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentStr);
                Body = contentDic ?? new Dictionary<string, object>();
            }
            catch
            {

            }
        }

        private void BuildImageBody(HttpContent content, string? mediaType)
        {
            ContentStr = mediaType;
            byte[] imageBytes = content.ReadAsByteArrayAsync().Result;
            if (!imageBytes.HasItem())
            {
                Message = "Get image response faild, image content is empty";
            }
            Body = new Dictionary<string, object>();
            Body.AddOrUpdate("imageContent", imageBytes);
        }

        private string BuildBodyFromGzip(HttpContent httpContent)
        {
            var contentStream = httpContent.ReadAsStreamAsync().Result;
            return Gzip.Decompress(contentStream);
            //var resString = httpContent.ReadAsStringAsync().Result;
            //return resString;
        }

        #endregion Body

        #region Log

        public void LogBody()
        {

        }

        #endregion Log
    }
}
