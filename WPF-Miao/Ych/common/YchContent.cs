using HttpProcessor.Content;
using System;
using System.Linq;
using Utils;
using Utils.stringBuilder;
using Ych.session;

namespace Ych.common
{
    internal class YchContent : HttpStringContent
    {
        public YchContent(string url) : base(url)
        {
            BuildUrl(url);
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildUrl(string url)
        {
            var baseUrl = MainSession.PlatformSession.GetString(Constants.HospitalId);
            RequestUrl = $"{baseUrl}{url}";
        }

        protected virtual void BuildHeader()
        {
            var baseUrl = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var host = baseUrl.Replace("http://", "www.");

            AddHeader("Host", host);
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("Origin", baseUrl);
            AddHeader("openId", MainSession.OpenId);
            //AddHeader("X-Access-Token", "bd7d3998d67b4cd58a8f884cd1ca7277");
            AddHeader("X-Access-Token", "5845df5db5a348dc89684c99813424fa"); //漕湖街道社区卫生服务中心
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Referer", $"{baseUrl}/wechatclient/?openId={MainSession.OpenId}");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }


        protected string GetContentsMD5()
        {
            try
            {
                var contentValues = Content.Values.Select(x => x.NotNullString()).ToArray();
                var contentValuesString = string.Join(string.Empty, contentValues);

                var contentsMD5 = Encryptor.ToMD5String(contentValuesString).ToLower();

                return contentsMD5;
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, ex.Message);
                return String.Empty;
            }
        }
    }
}
