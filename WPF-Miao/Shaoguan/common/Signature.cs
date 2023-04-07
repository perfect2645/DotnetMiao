using Shaoguan.session;
using System;
using System.Collections.Generic;
using Utils;

namespace Shaoguan.common
{
    internal class Signature
    {
        public string machine_code { get; set; }
        public long timestamp { get; set; }
        public string token { get; set; }

        public Signature(string machineCode, long ts, string tokn)
        {
            machine_code = machineCode;
            timestamp = ts;
            token = tokn;
        }

        private string _signJson;
        public string SignJson
        {
            get 
            { 
                if (!string.IsNullOrEmpty(_signJson))
                {
                    return _signJson;
                }
                return BuildSignJson();
            }
        }

        private string BuildSignJson()
        {
            try
            {
                var signDic = new Dictionary<string, object>();
                signDic.Add("machine_code", machine_code);
                signDic.Add("timestamp", timestamp);
                signDic.Add("token", token);

                return signDic.ToJson();
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, ex.Message);
                return string.Empty;
            }
        }
    }
}
