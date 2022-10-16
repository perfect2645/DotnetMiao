using Darunfa.session;
using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Darunfa.submit
{
    internal class SubmitContent : HttpStringContent
    {
        public SubmitContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "yx.feiniu.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Referer", "https://servicewechat.com/wx08cc6bd15fabfa53/84/page-frame.html");
            AddHeader("Accept-Encoding", "gzip,compress,br,deflate");
        }

        private void BuildContent()
        {
            AddContent("apiVersion", "t141");
            AddContent("appVersion", "1.5.1");
            AddContent("areaCode", "CS000016");
            AddContent("channel", "online");
            AddContent(MainSession.UserSession, Constants.ClientId);
            AddContent(MainSession.UserSession, Constants.DeviceId);
            AddContent("time", DateTimeUtil.GetNow().ToInt());
            AddContent("reRule", "4");
            AddContent("token", "c5d372fd360d5f7f2dbdae25e08cb394");
            AddContent("viewSize", "720x1184");
            AddContent("networkType", "wifi");
            AddContent("isSimulator", false);
            AddContent("osType", "4");
            AddContent("scopeType", 1);
            AddContent("businessType", 2);
            AddContent("businessId", "30520001");
            AddContent("deliveryCircleType", "2");
            AddContent("body", BuildBody());
        }

        private Dictionary<string, object> BuildBody()
        {
            var body = new Dictionary<string, object>();
            body.AddOrUpdate(MainSession.ShopSession, Constants.StoreId);
            body.AddOrUpdate(MainSession.ShopSession, Constants.AddrId);
            body.AddOrUpdate("pay_code", "45");
            body.AddOrUpdate("pay_pwd", "");
            body.AddOrUpdate("phone_model", "iPhone X (GSM+CDMA)<iPhone10,3>");
            body.AddOrUpdate("orderNotesId", "");
            body.AddOrUpdate("preCashCard", "");
            body.AddOrUpdate("freshVoucherSeqs", new { });
            body.AddOrUpdate("itemsVoucherSeqs", new { });
            body.AddOrUpdate("proofSeqs", new { });
            body.AddOrUpdate("deductVoucherSeqs", new { });
            body.AddOrUpdate("packageExpress", BuildPackageExpress());

            return body;
        }

        private Dictionary<string, object> BuildPackageExpress()
        {
            var packageExpress = new Dictionary<string, object>();
            var storeDay = $"{MainSession.ShopSession[Constants.StoreId]}+day";
            packageExpress.AddOrUpdate(storeDay, BuildStoreDay());

            return packageExpress;
        }

        private Dictionary<string, object> BuildStoreDay()
        {
            var storeDay = new Dictionary<string, object>();
            storeDay.AddOrUpdate(Constants.DeliveryDay, MainSession.BuildDeliveryDay());
            storeDay.AddOrUpdate(Constants.DeliveryTime, MainSession.BuildDeliveryTime());
            storeDay.AddOrUpdate(Constants.DeliveryTime, MainSession.BuildDeliveryTime());

            return storeDay;
        }
    }
}
