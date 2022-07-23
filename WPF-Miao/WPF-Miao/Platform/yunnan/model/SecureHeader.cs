using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utils;
using WPF_Miao.Platform.yunnan.getTimestamp;
using WPF_Miao.Platform.yunnan.session;

namespace WPF_Miao.Platform.yunnan.model
{
    internal class SecureHeader
    {
        private GetTimestampController _timeContra;

        public Dictionary<string, string> SecurityHeaderDic = new Dictionary<string, string>();

        public SecureHeader(GetTimestampController timeContra)
        {
            _timeContra = timeContra;
            //["X-Ca-Key", "X-Ca-Nonce", "X-Ca-Timestamp", "X-Content-MD5", "X-Service-Id", "X-Service-Method"]
            SecurityHeaderDic.Add(Constants.XCaKey, Constants.CAKEY);
            SecurityHeaderDic.Add(Constants.XCaNonce, string.Empty);
            SecurityHeaderDic.Add(Constants.XCaTimestamp, string.Empty);
            SecurityHeaderDic.Add(Constants.XContentMD5, string.Empty);
            SecurityHeaderDic.Add(Constants.XServiceId, "appoint.requestAppointRecordService");
            SecurityHeaderDic.Add(Constants.XServiceMethod, "addAppointRecord");
        }

        public async Task BuildHeader()
        {
            var herderTimestamp = await _timeContra.GetTimestamp();
            SetTimestamp(herderTimestamp.Timestamp);
            SetXCaNonce(herderTimestamp.XCaNonce);
        }

        private void SetTimestamp(string time)
        {
            SecurityHeaderDic[Constants.XCaTimestamp] = time;
        }

        private void SetXCaNonce(string xCaNonce)
        {
            SecurityHeaderDic[Constants.XCaNonce] = xCaNonce;
        }

        public void SetXContentMD5(string contentJson)
        {
            var base64Md5 = Encryptor.ToBase64Md5(contentJson);
            SecurityHeaderDic[Constants.XContentMD5] = base64Md5;
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
    }
}
