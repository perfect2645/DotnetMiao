using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.getTimestamp;
using WPF_Miao.Platform.yunnan.session;

namespace WPF_Miao.Platform.yunnan.model
{
    internal class SecureHeader
    {
        private GetTimestampController _timeContra;

        public Dictionary<string, string> SecurityHeaderDic = new Dictionary<string, string>();
        public Action<string> SetMd5Action { get; set; }

        public SecureHeader(GetTimestampController timeContra)
        {
            _timeContra = timeContra;

            SetMd5Action = new Action<string>(SetMd5);
            SecurityHeaderDic.Add("X-Ca-Key", Constants.CAKEY);
            SecurityHeaderDic.Add("X-Ca-Nonce", string.Empty);
            SecurityHeaderDic.Add("X-Ca-Timestamp", string.Empty);
            SecurityHeaderDic.Add("X-Content-MD5", string.Empty);
        }

        public async Task BuildHeader()
        {
            var herderTimestamp = await _timeContra.GetTimestamp();
            SetTimestamp(herderTimestamp.Timestamp);
            SetXCaNonce(herderTimestamp.XCaNonce);
        }

        private void SetTimestamp(string time)
        {
            SecurityHeaderDic["X-Ca-Timestamp"] = time;
        }

        private void SetXCaNonce(string xCaNonce)
        {
            SecurityHeaderDic["X-Ca-Nonce"] = xCaNonce;
        }

        private void SetMd5(string jsonData)
        {
            SecurityHeaderDic["X-Content-MD5"] = jsonData;
        }
    }
}
