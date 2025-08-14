using HttpProcessor.Client;
using Jingjiang.common;
using Jingjiang.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Jingjiang.login;
using System;
using System.Text;

namespace Jingjiang.login
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(JingjiangLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(JingjiangLogin user)
        {
            try
            {
                var userName = user.UserName;
                var idCard = user.IdCard;
                var phone = user.Phone;

                var sb = new StringBuilder();

                sb.AppendLine("******** 用户信息 *********");

                sb.AppendLine($"姓名 - {userName}");
                sb.AppendLine($"身份证 - {idCard}");
                sb.AppendLine($"phone - {phone}");
                sb.AppendLine("**************************************");

                MainSession.PrintLogEvent.Publish(user, sb.ToString());
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }
    }
}
