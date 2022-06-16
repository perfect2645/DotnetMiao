using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace Utils
{
    public static class ObjectExcension
    {
        public static IDictionary<string, object> Dic(this object obj)
        {
            if (obj is IDictionary<string, object>)
            {
                return obj as IDictionary<string, object>;
            }
            return null;
        }

        public static object Dic(this object obj, string key)
        {
            if (obj is IDictionary<string, object>)
            {
                var dic = obj as IDictionary<string, object>;
                return dic[key];
            }
            if (obj is JObject)
            {
                return GetJValue(obj as JObject, key);
            }
            return obj;
        }

        private static object GetJValue(JObject obj, string key)
        {
            var jObj = obj as JObject;

            var first = jObj.First;
            var result = first[key];
            if (jObj.First is JObject)
            {
                //GetJValue(jObj.First, key);
            }

            var jvalue = jObj.GetValue(key);
            return obj;
        }
    }
}
