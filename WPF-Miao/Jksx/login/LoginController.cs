using Base.session;
using Jksx.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Utils;
using Utils.Encode;
using Utils.stringBuilder;

namespace Jksx.login
{
    public class LoginController
    {
        internal void DecodeLoginData(JksxLogin userInfo)
        {
            try
            {
                var aesDe = AesEncode.AESDecrypt(userInfo.LoginEncode, Constants.EncodeKey, Constants.EncodeIV);
                var loginData = aesDe.text.ToObjDic();

                SaveLoginInfo(loginData, userInfo);
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"DecodeLoginData error - {ex.Message}");
                Logging.GLog.Logger.Error(ex);
            }
        }

        private void SaveLoginInfo(Dictionary<string, object> loginData, JksxLogin userInfo)
        {
            var loginInfo = loginData.GetString("result");
            var lgoinInfoDic = loginInfo.ToObjDic();

            var mainId = lgoinInfoDic.GetString("userid");

            userInfo.MainId = mainId;
        }
    }
}
