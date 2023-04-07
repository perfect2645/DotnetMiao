using System.Text.Json;

namespace Utils.json
{
    public static class JsonAnalysis
    {
        public static List<Dictionary<string, object>> JsonToDicList(JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                var jsonArr = jsonElement.EnumerateArray();
                return JsonArrayToDic(jsonArr);
            }

            return new List<Dictionary<string, object>>();
        }
        public static Dictionary<string, object> JsonToDic(JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.Object)
            {
                var jsonObject = jsonElement.EnumerateObject();
                return JsonObjToDic(jsonObject);
            }

            return new Dictionary<string, object>();
        }

        private static Dictionary<string, object> JsonObjToDic(JsonElement.ObjectEnumerator jsonObject)
        {
            var jsonDic = new Dictionary<string, object>();
            foreach(var item in jsonObject)
            {
                var valueDic = JsonToString(item.Value);
                jsonDic.AddOrUpdate(item.Name, valueDic);
            }

            return jsonDic;
        }

        public static string JsonToString(JsonElement jsonElement)
        {
            return jsonElement.ToString();
        }

        private static List<Dictionary<string, object>> JsonArrayToDic(JsonElement.ArrayEnumerator jsonArr)
        {
            var jsonDicList = new List<Dictionary<string, object>>();
            foreach(var item in jsonArr)
            {
                jsonDicList.Add(JsonToDic(item));
            }

            return jsonDicList;
        }

        public static List<Dictionary<string, object>> JsonToDicList(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
            {
                return new List<Dictionary<string, object>>();
            }

            var jsonElement = JsonDocument.Parse(jsonStr).RootElement;
            return JsonToDicList(jsonElement);
        }
    }
}
