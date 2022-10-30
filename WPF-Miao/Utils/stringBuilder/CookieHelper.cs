using System.Web;

namespace Utils.stringBuilder
{
    public static class CookieHelper
    {
        public static Dictionary<string, object> ArrToDic(this string[] strings)
        {
            var dic = new Dictionary<string, object>();
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

        public static Dictionary<string, object> CookieToDic(this string cookie)
        {
            if (string.IsNullOrEmpty(cookie))
            {
                return null;
            }

            var cookieArr = cookie.Split(';');

            var dic = ArrToDic(cookieArr);

            return dic;
        }

        public static Dictionary<string, object> UrlToDic(this string url)
        {
            var uri = new Uri(url);
            var queryList = HttpUtility.ParseQueryString(uri.Query);

            var resultDic = new Dictionary<string, object>();
            foreach (var item in queryList)
            {
                var key = item.NotNullString();
                var value = queryList[key];
                resultDic.AddOrUpdate(key, value);
            }

            return resultDic;
        }
    }
}
