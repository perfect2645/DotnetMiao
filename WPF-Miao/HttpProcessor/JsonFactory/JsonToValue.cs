using Newtonsoft.Json;
using System.Text.Json;
using static System.Text.Json.JsonElement;

namespace HttpProcessor.JsonFactory
{
    public static class JsonToValue
    {
        public static string GetStringValue(this JsonElement jsonElement, string key)
        {
            if (jsonElement.ValueKind == JsonValueKind.Object)
            {
                return GetValueFromObject(jsonElement, key)?.ToString();
            }
            

            return null;
        }

        private static object GetValueFromObject(JsonElement jsonElement, string key)
        {
            JsonElement prop = default(JsonElement);
            if (!jsonElement.TryGetProperty(key, out prop))
            {
                return GetInnerValue(jsonElement, key);
                //return jsonElement.ToString();
            }
            if (prop.ValueKind == JsonValueKind.Object)
            {
                var innerValue = GetValueFromObject(prop, key);
                return innerValue;
            }
            if (prop.ValueKind == JsonValueKind.Number)
            {
                return prop.GetInt64();
            }
            if (prop.ValueKind == JsonValueKind.String)
            {
                return prop.GetString();
            }
            return null;
        }

        private static string GetInnerValue(ArrayEnumerator arrayEnumerator, string key)
        {
            foreach (var innerObj in arrayEnumerator)
            {
                var result = GetStringValue(innerObj, key);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        private static object GetInnerValue(JsonElement jelement, string key)
        {
            var jEnumerate = jelement.EnumerateObject();

            var innerObjs = jEnumerate.Where(x => x.Value.ValueKind == JsonValueKind.Object);
            foreach (var item in innerObjs)
            {
                if (item.Value.ValueKind == JsonValueKind.Object)
                {
                    var result = GetValueFromObject(item.Value, key);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return "";
        }
    }
}
