using Dalian.login;
using Dalian.session;
using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using Utils;
using Utils.datetime;

namespace Dalian.common
{
    internal class DalianContent : HttpStringContent
    {
        public DalianLogin User { get; private set; }
        public string Path { get; set; }
        public Dictionary<string, object> Parameters { get; private set; }
        public Dictionary<string, object> RequestData { get; private set; }

        public DalianContent(string baseUrl, DalianLogin user) : base(baseUrl)
        {
            ContentType= "application/json";
            User = user;
            Parameters = new Dictionary<string, object>();
            RequestData = new Dictionary<string, object>();
            BuildRequestData();
            BuildHeader();
        }

        protected virtual void BuildRequestData()
        {
            var appid = MainSession.PlatformSession.GetString(Constants.AppId);
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            Parameters.Add(Constants.AppId, appid);
            Parameters.Add("openId", User.OpenId);
            Parameters.Add("hospId", hosId.ToInt());
            Parameters.Add("hospitalId", hosId.ToInt());

            var jsonParam = Parameters.ToJson();
            var encodeReq = JsReader.GetERequestData(Path, jsonParam);

            AddContent("requestData", RequestData.GetString("requestData"));
        }

        protected virtual void BuildHeader()
        {
            var appid = MainSession.PlatformSession.GetString(Constants.AppId);
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddHeader("Host", "hlwyy.dlfeyljt.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("appId", appid);
            AddHeader("appKind", "1");
            AddHeader("appMark", "1");
            AddHeader("appType", "5");
            AddHeader("appVersion", "1.0");
            AddHeader("hospitalId", hosId);
            AddHeader("token", User.Token);
            AddHeader("userId", User.UserId);
            AddHeader("Referer", "https://servicewechat.com/wxa794c2a4fcfeb7f4/44/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("nonce", RequestData.GetString("nonce"));
            AddHeader("signature", RequestData.GetString("signature"));
            AddHeader("timestamp", RequestData.GetString("timestamp"));
        }
    }
}
