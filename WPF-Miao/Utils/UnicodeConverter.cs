using System.Text;
using System.Web;

namespace Utils
{
    public static class UnicodeConverter
    {
        #region UrlDecode(对Url进行编码)

        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string Decode(string content, bool isUpper = false)
        {
            return Decode(content, Encoding.UTF8, isUpper);
        }

        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string Decode(string content, Encoding encoding, bool isUpper = false)
        {
            var result = HttpUtility.UrlDecode(content, encoding);
            if (!isUpper)
                return result;
            return GetUpperEncode(result);
        }

        /// <summary>
        /// 获取大写编码字符串
        /// </summary>
        /// <param name="encode">字串</param>
        /// <returns></returns>
        private static string GetUpperEncode(string encode)
        {
            var result = new StringBuilder();
            int index = int.MinValue;
            for (int i = 0; i < encode.Length; i++)
            {
                string character = encode[i].ToString();
                if (character == "%")
                    index = i;
                if (i - index == 1 || i - index == 2)
                    character = character.ToUpper();
                result.Append(character);
            }
            return result.ToString();
        }

        #endregion

        #region UrlEncode


        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string Encode(string content, bool isUpper = false)
        {
            return Encode(content, Encoding.UTF8, isUpper);
        }

        public static string EncodeOriginal(string content, bool isUpper = false)
        {
            return EncodeOriginal(content, Encoding.UTF8, isUpper);
        }

        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string Encode(string content, Encoding encoding, bool isUpper = false)
        {
            var result = HttpUtility.UrlEncode(content, encoding);
            result = result.Replace("+", "%20");
            result = result.Replace("%5cu002B", "%2B");
            result = result.Replace("%5cu003C", "%3C");
            result = result.Replace("%5cu003E", "%3E");
            if (!isUpper)
                return result;
            return GetUpperEncode(result);
        }

        public static string EncodeOriginal(string content, Encoding encoding, bool isUpper = false)
        {
            var result = HttpUtility.UrlEncode(content, encoding);
            if (!isUpper)
                return result;
            return GetUpperEncode(result);
        }
        #endregion 
    }
}
