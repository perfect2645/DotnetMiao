using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentContent : HttpClientContentBase
    {
        public string Cookie
        {
            get
            {
                return "client-id=81150288; isNewUser=false; api=WX; tk=3715d2e8-7cbe-43cf-b3bb-f9442b3aaa5e; acw_tc=2f61f26b16580288796837140e41ac0dba2e81e5102b9a01bd8dbfaab2ac56";
            }
        }

        public AppointmentContent() : base()
        {
            BuildDefaultHeaders();
            BuildDefaultContents();
        }

        #region Headers

        private void BuildDefaultHeaders()
        {
            HttpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            //HttpRequestMessage.Headers.Add("X-Service-Id", "appoint.requestAppointRecordService");
            HttpRequestMessage.Headers.Add("Origin", "https://weixin.ngarihealth.com");
            HttpRequestMessage.Headers.Add("content-Type", "application/json");
            HttpRequestMessage.Headers.Add("User-Agent", @"Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 Language/zh_CN");

            //HttpRequestMessage.Headers.Add("X-Service-Method", "saveBuriedPointList");
            HttpRequestMessage.Headers.Add("Host", "weixin.ngarihealth.com");
            HttpRequestMessage.Headers.Add("Cookie", Cookie);// TODO
            HttpRequestMessage.Headers.Add("Referer", "https://weixin.ngarihealth.com/weixin/wx/mp/wxf119c4ff0a602d44/index.html?module=appointHomePage&source=wx_menu&code=021Vx1100KZ0cO1JSH300LtCKw2Vx11S&state=STATE");
            HttpRequestMessage.Headers.Add("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            HttpRequestMessage.Headers.Add("encoding", "utf-8");
            HttpRequestMessage.Headers.Add("Accept", "*/*"); // TODO
        }

        #endregion Headers

        #region Contents

        private void BuildDefaultContents()
        {
            var timeNow = DateTime.Now;
            var timeStart = timeNow.AddMinutes(-1);
            Contents.Add("mpiid", "2c90823e8147f0a4018157f291d845f8"); //Dynamic
            Contents.Add("patientName", "崔法威");
            Contents.Add("certId", "2****************0");
            Contents.Add("linkTel", "139****7525");
            Contents.Add("organAppointId", "");
            Contents.Add("appointSourceId", 1363693193);//Dynamic
            Contents.Add("organId", "1001176");     //Dynamic
            Contents.Add("appointDepartId", "1083");//Dynamic
            Contents.Add("appointDepartName", "新生儿随访门诊");
            Contents.Add("doctorId", 162516);       //Dynamic
            Contents.Add("workDate", "2022-07-20 00:00:00");//Dynamic
            Contents.Add("workType", 2);//Dynamic
            Contents.Add("sourceType", 1);//Dynamic
            Contents.Add("startTime", "2022-07-20 14:00:00");//Dynamic
            Contents.Add("endTime", "2022-07-20 14:30:00");//Dynamic
            Contents.Add("orderNum", 9);//Dynamic
            Contents.Add("appointRoad", 5);//Dynamic
            Contents.Add("appointStatus", 0);
            Contents.Add("appointPath", 9);//Dynamic
            Contents.Add("appointUser", "2c90823e8147f0a4018157f291d845f8");//Dynamic
            Contents.Add("appointName", "崔法威");
            Contents.Add("appointOragn", "");
            Contents.Add("clinicPrice", 5.5);
            Contents.Add("transferId", 0);
            Contents.Add("sourceLevel", 1);
            Contents.Add("clinicId", "");
            Contents.Add("ifCreateFollowPlan", 1);
            Contents.Add("cardId", "210302198608163030"); // 身份证
            Contents.Add("triggerId", null);
            Contents.Add("medInsureCarId", "");
            Contents.Add("appointRecordExt", new Dictionary<string, object> {
                { "illSummaryTxt" , "" },
                { "thirdChannel", null}
            });
            Contents.Add("analyzeNvcData", "");
            Contents.Add("ruleString", "");
            Contents.Add("isRealTime", 0);

            //Contents.Add("startTime", timeStart.ToString("yyyy-MM-dd HH:mm:ss")); //TODO
        }

        #endregion Contents
    }
}
