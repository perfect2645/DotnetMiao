using Newtonsoft.Json;

namespace HttpProcessor.JsonFactory
{
    public static class JsonDicConverter
    {
        public static Dictionary<string, string> ToDic(this string jsonStr)
        {
            var contentDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
            return contentDic;
        }

        public static Dictionary<string, object> ToObjDic(this string jsonStr)
        {
            var contentDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
            return contentDic;
        }
    }
}
