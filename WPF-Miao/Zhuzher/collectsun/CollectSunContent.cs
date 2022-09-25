using HttpProcessor.Content;
using System;
using Utils.datetime;

namespace Zhuzher.collectsun
{
    internal class CollectSunContent : HttpStringContent
    {
        private const string _url = "https://chaos.4009515151.com/market/api/notice/scene";
        public CollectSunContent() : base(_url)
        {
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "chaos.4009515151.com");
            AddHeader("X-channel", "zhuzher");
            AddHeader("Zhuzher-Project-Code", "zh-CN,zh-Hans;q=0.9");
            //AddHeader("Authorization", "Bearer d0645e17-98cf-48d7-af3b-80a652ca4dd2");
            AddHeader("Accept", "*/*");
            AddHeader("Zhuzher-City-Code", "210200");
            AddHeader("X-Version", "5.3.92");
            AddHeader("Accept-Language", "zh-Hans-CN;q=1.0, ja-JP;q=0.9, en-CN;q=0.8");
            AddHeader("X-API-Version", "20220902");
            //AddHeader("Accept-Encoding", @"VKProprietorAssistant/5.3.92 (com.zhuzher; build:2022090201; iOS 15.2.1) Alamofire/5.6.2");
            //AddHeader("X - Device - ID", "3C46E82A-585F-46ED-B6C0-267CD91067F0");
            AddHeader("X-Platform", "iOS");
            AddHeader("Connection", "keep-alive");
            AddHeader("Zhuzher-Project-Role", "0");
        }

        private void BuildContent()
        {
            //AddContent("matchParam", @"https:\/\/uiis.4009515151.com\/fg_activity\/template?id=2180");
            var nowTimestapm = DateTimeUtil.GetTimeStamp();
            AddContent("requestId", nowTimestapm);
            AddContent("value", string.Empty);
        }

    }
}
