using Newtonsoft.Json;

namespace HttpProcessor.JsonFactory
{
    public class JsonSerialize<T> where T : class
    {
        public static string ToJson(T classToJson) => JsonConvert.SerializeObject(classToJson, HttpJsonConverter.Settings);
    }
}
