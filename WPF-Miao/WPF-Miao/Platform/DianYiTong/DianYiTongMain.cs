using HttpProcessor.Container;
using WPF_Miao.Platform.DianYiTong.Hospital;

namespace WPF_Miao.Platform
{
    internal class DianYiTongMain
    {
        public DianYiTongMain()
        {
            InitHttpController();
        }

        private void InitHttpController()
        {
            HttpServiceController.AddTransientService<HospitalSummaryClient, HospitalSummaryHandler>();
        }

        ///*
        //* GET https://newdytapi.ynhdkc.com/index/hospital/hoscate?cate_type=4 HTTP/1.1
        //* Host: newdytapi.ynhdkc.com
        //* Connection: keep-alive
        //* Accept: application/json, text/plain, */*
        //* Origin: https://appv2.ynhdkc.com
        //* Authorization: DYT eyJhbGciOiJIUzI1NiJ9.eyJ3ZWNoYXRfaWQiOjUxNjQ0OTMsInN1YnNjcmliZSI6MCwiZHpqX3N1YnNjcmliZSI6MCwib3BlbmlkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInRoaXJkX3VzZXJfaWQiOiIiLCJpc3MiOiJkeXQiLCJuZXdfc3Vic2NyaWJlIjoxLCJuZXdfb3BlbmlkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInVzZXJfaWQiOjM2NjkwNTMsIndlY2hhdF9vcGVuX2lkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInVuaW9uX2lkIjoib05RejQwUnFpaVdzVkQ2Q2E3N3ltQ2JZaWxuQSIsIm1vY2tfb3BlbmlkIjpmYWxzZSwibWluaV9vcGVuaWQiOiIiLCJleHAiOjE2NTAxMTczMDUsImlhdCI6MTY1MDExMTcwNX0.wu9GNerUT-5Y_ASFjblpyH6Q2b7YhqZbhNBcnbNl1Eg
        //* User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63060012)
        //* x-uuid: D6383B9C33C1444A281544868EE6FB6E
        //* Sec-Fetch-Site: same-site
        //* Sec-Fetch-Mode: cors
        //* Sec-Fetch-Dest: empty
        //* Referer: https://appv2.ynhdkc.com/registration_vaccine_list
        //* Accept-Encoding: gzip, deflate, br
        //*  Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
        //*/
    }
}
