using System.Text.Json;
using Utils.json;

namespace Utils
{
    public static class DictionaryUtils
    {
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value) where TKey : notnull
        {
            dic = dic ?? new Dictionary<TKey, TValue>();
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }

        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd) where TKey : notnull
        {
            dic = dic ?? new Dictionary<TKey, TValue>();

            if (dicToAdd == null)
            {
                return;
            }

            foreach(var pair in dicToAdd)
            {
                dic.AddOrUpdate(pair.Key, pair.Value);
            }
        }


        public static string ToJson(this Dictionary<string, object> dic)
        {
            var json = JsonSerializer.Serialize(dic, JsonEncoder.JsonOption);

            return json;
        }

    }
}