using Gzjk.common;
using Gzjk.login;
using System.Collections.Generic;
using Utils;
using Utils.Encode;

namespace Gzjk.appointment
{
    internal class YuyueContent : GzjkContent
    {
        private static string baseUrl = "https://api.cn2030.com/sc/api/User/OrderPost";
        public Order Order { get; private set; }
        public YuyueContent(Order order, GzjkLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            var contentDic = new Dictionary<string, object>();

            contentDic.Add("birthday", Order.Birthday);
            contentDic.Add("tel", Order.Tel);
            contentDic.Add("sex", Order.Sex);
            contentDic.Add("cname", Order.UserName);
            contentDic.Add("doctype", Order.Doctype);
            contentDic.Add("idcard", Order.Idcard);
            contentDic.Add("mxid", Order.Mxid);
            contentDic.Add("date", Order.Date);
            contentDic.Add("pid", Order.Pid);
            contentDic.Add("Ftime", Order.Ftime);
            contentDic.Add("guid", Order.Guid);

            var jsonContent = contentDic.ToJson();

            //var encodeContent = AesEncode.AESEncrypt(jsonContent, );

            AddContent("", jsonContent);
        }
    }
}
