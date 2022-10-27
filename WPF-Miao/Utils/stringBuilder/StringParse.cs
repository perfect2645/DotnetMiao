using Newtonsoft.Json;

namespace Utils.stringBuilder
{
    public static class StringParse
    {
        public static long ToLong(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt64(str);
        }

        public static int ToInt(this string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        public static string NotNullString(this object? source)
        {
            var strSource = source?.ToString();
            return strSource ?? string.Empty;
        }

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

        public static bool ToBool(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return Convert.ToBoolean(str);
        }
    }
}
