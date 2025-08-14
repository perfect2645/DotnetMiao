using Jksx.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Encode;
using Utils.stringBuilder;

namespace Jksx.common
{
    public static class Encotor
    {
        public static string EncodeFromDic(Dictionary<string, object> data)
        {
            try
            {
                var jsonStr = data.ToJson();
                var aesEn = AesEncode.AESEncrypt(jsonStr, Constants.EncodeKey, Constants.EncodeIV);

                return aesEn.text;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(data, $"Encoder.EncodeFromDic error - {ex.Message}");
                Logging.GLog.Logger.Error(ex);

                return null;
            }
        }

        public static Dictionary<string, object> DecodeToDic(string data)
        {
            try
            {
                var aesDe = AesEncode.AESDecrypt(data, Constants.EncodeKey, Constants.EncodeIV);
                var dataDic = aesDe.text.ToObjDic();

                return dataDic;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(data, $"Encoder.EncodeFromDic error - {ex.Message}");
                Logging.GLog.Logger.Error(ex);

                return null;
            }
        }

        public static List<Dictionary<string, object>> DecodeToDicList(string data)
        {
            try
            {
                var aesDe = AesEncode.AESDecrypt(data, Constants.EncodeKey, Constants.EncodeIV);
                var dataDicList = aesDe.text.ToObjDicList();

                return dataDicList;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(data, $"Encoder.EncodeFromDic error - {ex.Message}");
                Logging.GLog.Logger.Error(ex);

                return null;
            }
        }
    }
}
