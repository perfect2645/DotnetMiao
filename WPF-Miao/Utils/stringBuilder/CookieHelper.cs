namespace Utils.stringBuilder
{
    public static class CookieHelper
    {
        public static Dictionary<string, string> ArrToDic(this string[] strings)
        {
            var dic = new Dictionary<string, string>();
            foreach (var item in strings)
            {
                if (!item.Contains("="))
                {
                    continue;
                }
                var keyValue = item.Split('=');
                dic.AddOrUpdate(keyValue[0].Trim(), keyValue[1].Trim());
            }

            return dic;
        }

        public static Dictionary<string, string> CookieToDic(this string cookie)
        {
            if (string.IsNullOrEmpty(cookie))
            {
                return null;
            }

            var cookieArr = cookie.Split(';');

            var dic = ArrToDic(cookieArr);

            return dic;
        }
    }
}
