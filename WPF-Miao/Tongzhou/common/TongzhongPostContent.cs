using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tongzhou.login;
using Utils;
using Utils.json;
using Tongzhou.session;
using System.Text.Json;

namespace Tongzhou.common
{
    internal class TongzhongPostContent : TongzhouContent
    {
        public string XCaMethod { get; set; }
        public Dictionary<string, string> SecurityHeaderDic = new Dictionary<string, string>();

        public TongzhongPostContent(string baseUrl, string method, string methodId, TongzhouLogin user) : base(baseUrl, user)
        {
            XCaMethod = method;
            BuildSignHeaders();
        }

        private void BuildSignHeaders()
        {
            AddHeader("X-Ca-Signature", "");
            AddHeader("X-Ca-Nonce", "");
            AddHeader("X-Ca-Timestamp", "");
            AddHeader("X-Service-Encrypt", "1");
            AddHeader("X-Service-Method", XCaMethod);
            AddHeader("X-Ca-Key", "ngari-wx");
            AddHeader("X-Content-MD5", "");
            AddHeader("encoding", "utf-8");
            AddHeader("X-Service-Id", "eh.familyMemberService");
        }

        public void SetXContentMD5(string contentJson)
        {
            var base64Md5 = Encryptor.ToBase64Md5(contentJson);
            SecurityHeaderDic["X-Content-MD5"] = base64Md5;
        }

        public void BuildXCaSignature()
        {
            var sb = new StringBuilder();
            foreach (var header in SecurityHeaderDic)
            {
                sb.Append(header.Key.ToLower());
                sb.Append(":");
                sb.Append(header.Value);
                sb.Append("&");
            }
            var textToSign = sb.ToString().TrimEnd('&');
            var sha256 = Encryptor.HmacSHA256(textToSign, Constants.CASECRET);
            var signature = Encryptor.EncryptBase64(sha256);

            SecurityHeaderDic.Add(Constants.XCaSignature, signature);
        }

        public StringContent GetJsonContent(Dictionary<string, object> contents, bool isArr = false)
        {
            var jsonContent = JsonSerializer.Serialize(contents, JsonEncoder.JsonOption);
            if (isArr)
            {
                jsonContent = $"[{jsonContent}]";
            }
            return new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }
    }
}
