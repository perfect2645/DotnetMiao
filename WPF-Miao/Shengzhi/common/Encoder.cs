using System.Collections.Generic;
using Utils;
using Utils.stringBuilder;

namespace Shengzhi.common
{
    internal static class Encoder
    {
        public static string GetQyCheckSuffix(Dictionary<string, object> source, string timeStamp)
        {
            var content = SbHelper.GetStringContent(source);
            var contentMd5 = Encryptor.ToMD5String(content);

            var qyCheckStr = $"{contentMd5}@@{timeStamp}";
            var qyCheckMd5 = Encryptor.ToMD5String(qyCheckStr);

            return qyCheckMd5;
        }

        public static string GetQyCheckSuffix(string url, string timeStamp)
        {
            var query = url.UrlToDic();
            return Encoder.GetQyCheckSuffix(query, timeStamp);
        }
    }
}
