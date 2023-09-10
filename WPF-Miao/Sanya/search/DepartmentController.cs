using Sanya.appointment;
using Sanya.session;
using HttpProcessor.Client;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;

namespace Sanya.search
{
    internal class DepartmentController : HttpClientBase
    {
        public string Date { get; set; }
        public DepartmentController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchDepartment()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new DepartmentContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDepartment - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var message = root.GetProperty("message").GetString();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDepartment失败: code={code}, message = {message}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDepartment失败: results is empty");
                    return false;
                }
                var classList = data.GetProperty("classList");

                return CheckSaveVaccine(classList);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到SearchDepartment - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveVaccine(JsonElement classListItem)
        {
            var classList = JsonAnalysis.JsonToDicList(classListItem);

            if (!classList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"还没开始");
                return false;
            }

            var targetClass = classList.FirstOrDefault(x => x.GetString("className").Contains("疫苗预约"));
            var menuList = targetClass.GetString("menuList").ToObjDicList();

            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var targetDepartment = menuList.FirstOrDefault(x => x.GetString("resourceName").Contains(deptName));
            if (targetDepartment == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"没找到对应的疫苗（{deptName}），自动选择9价");
                targetDepartment = classList.FirstOrDefault(x => x.GetString("resourceName").Contains("九价"));
            }
            if (targetDepartment == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"没找到对应的疫苗，也没九价");
                return false;
            }

            var clientUrl = targetDepartment.GetString("clientUrl");

            if (string.IsNullOrEmpty(clientUrl))
            {
                MainSession.PrintLogEvent.Publish(this, $"clientUrl is empty, 没开始");
                return false;
            }

            var subscribeType = clientUrl.SplitToList("subscribeType=")[1];
            if(subscribeType.Contains("2c90812388d150370188d1934ed6003c"))
            {
                subscribeType = "2c90812388d150370188d1934ed6003c";
            }

            MainSession.PlatformSession[Constants.SubscribeType] = subscribeType;

            return true;
        }
    }
}