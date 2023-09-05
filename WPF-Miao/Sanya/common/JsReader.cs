

using JsInteract.ClearJs;
using Sanya.session;
using System;
using System.Collections.Generic;
using Utils;
using Utils.stringBuilder;

namespace Sanya.common
{
    internal static class JsReader
    {
        private static readonly JsEngine JsEngine;

        static JsReader()
        {
            JsEngine = new JsEngine();
            JsEngine.InitEngine("common/crypto-js.min.js");
            JsEngine.InitEngine("common/encode.js");
        }

        public static Dictionary<string, object> DecodeAesCbc(string input)
        {
            var requestDataDic = new Dictionary<string, object>();
            try
            {
                var result = JsEngine.Engine.Script.decryptByAes(input) as string;
                requestDataDic = result.ToObjDic();
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(input, $"DecodeAesCbc异常:{ex.Message}");
            }

            return requestDataDic;
        }

        public static string EncodeStr(string input)
        {
            try
            {
                var result = JsEngine.Engine.Script.encryptByAes(input) as string;
                return result.NotNullString();
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(input, $"EncodeStr异常:{ex.Message}");
                return null;
            }
        }

    }
}
