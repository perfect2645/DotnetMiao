using HttpProcessor.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Miao.Platform
{
    internal class SubmitOrder
    {
        /*        POST https://dytapi.ynhdkc.com/v1/appoint?hos_code=872018&dep_id=960&doc_id=3548&pat_id=21180400&user_id=3669053&schedule_id=1294772&cate_name= HTTP/1.1
            Host: dytapi.ynhdkc.com
            Connection: keep-alive
            Content-Length: 402
            Accept: application/json, text/plain, **//*
            Origin: https://appv2.ynhdkc.com
            Authorization: DYT eyJhbGciOiJIUzI1NiJ9.eyJ3ZWNoYXRfaWQiOjUxNjQ0OTMsInN1YnNjcmliZSI6MCwiZHpqX3N1YnNjcmliZSI6MCwib3BlbmlkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInRoaXJkX3VzZXJfaWQiOiIiLCJpc3MiOiJkeXQiLCJuZXdfc3Vic2NyaWJlIjoxLCJuZXdfb3BlbmlkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInVzZXJfaWQiOjM2NjkwNTMsIndlY2hhdF9vcGVuX2lkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInVuaW9uX2lkIjoib05RejQwUnFpaVdzVkQ2Q2E3N3ltQ2JZaWxuQSIsIm1vY2tfb3BlbmlkIjpmYWxzZSwibWluaV9vcGVuaWQiOiIiLCJleHAiOjE2NTA0NDM0NzYsImlhdCI6MTY1MDQzNzg3Nn0.IYrW3VVoDvvQgUaA6alvM3GamS_8OKFhD4TCSy2aInk
            User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63060012)
            x-uuid: 3E4BBCBF68FF8B3382B2DC3F0B51387E
            Content-Type: application/json
            Sec-Fetch-Site: same-site
            Sec-Fetch-Mode: cors
            Sec-Fetch-Dest: empty
            Referer: https://appv2.ynhdkc.com/registration_order_confirm
            Accept-Encoding: gzip, deflate, br
            Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7
            {
                "doc_name": "带状疱疹疫苗",
                "hos_name": "西山区马街社区卫生服务中心（疫苗）",
                "hos_code": "872018",
                "dep_name": "成人疫苗预约",
                "level_name": "",
                "dep_id": "960",
                "doc_id": "3548",
                "pat_id": 21180400,
                "schedule_id": 1294772,
                "jz_card": "",
                "sch_date": "2022-04-26",
                "time_type": "2",
                "info": "",
                "ghf": 0,
                "zlf": 0,
                "zjf": 0,
                "jz_start_time": 0,
                "amt": 0,
                "jz_card_type": 0,
                "queue_sn_id": "",
                "wechat_login": ""
            }
            HTTP/1.1 200 OK
            Date: Wed, 20 Apr 2022 08:02:12 GMT
            Content-Type: application/json; charset=utf-8
            Connection: keep-alive
            Set-Cookie: acw_tc=2760776c16504417321721514e8adc0967971b23b3c9d5efccb5fd07926932;path=/;HttpOnly;Max-Age=1800
            Server: nginx
            Vary: Accept-Encoding
            Access-Control-Allow-Origin: *
            Content-Length: 80

            {"code":1,"msg":"预约成功","data":{"order_id":"19725606","wxresult_url":""}}
        */
        public async void TrySubmitOrder()
        {
            var url = "https://dytapi.ynhdkc.com/v1/appoint?hos_code=872018&dep_id=960&doc_id=3548&pat_id=21180400&user_id=3669053&schedule_id=1294772&cate_name=";
            var jsonContent = "{\"doc_name\":\"带状疱疹疫苗\",\"hos_name\":\"西山区马街社区卫生服务中心（疫苗）\",\"hos_code\":\"872018\",\"dep_name\":\"成人疫苗预约\",\"level_name\":\"\",\"dep_id\":\"960\",\"doc_id\":\"3548\",\"pat_id\":21180400,\"schedule_id\":1294772,\"jz_card\":\"\",\"sch_date\":\"2022 - 04 - 26\",\"time_type\":\"2\",\"info\":\"\",\"ghf\":0,\"zlf\":0,\"zjf\":0,\"jz_start_time\":0,\"amt\":0,\"jz_card_type\":0,\"queue_sn_id\":\"\",\"wechat_login\":\"\"}";
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Host", "dytapi.ynhdkc.com");
            headers.Add("Connection", "keep-alive");
            headers.Add("Accept", "application/json, text/plain, **//*");
            headers.Add("Origin", "https://appv2.ynhdkc.com");
            headers.Add("Authorization", "DYT eyJhbGciOiJIUzI1NiJ9.eyJ3ZWNoYXRfaWQiOjUxNjQ0OTMsInN1YnNjcmliZSI6MCwiZHpqX3N1YnNjcmliZSI6MCwib3BlbmlkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInRoaXJkX3VzZXJfaWQiOiIiLCJpc3MiOiJkeXQiLCJuZXdfc3Vic2NyaWJlIjoxLCJuZXdfb3BlbmlkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInVzZXJfaWQiOjM2NjkwNTMsIndlY2hhdF9vcGVuX2lkIjoibzdMQ1g2SzRFTjJJYTJGeWdNd0RUWFBsYzUwMCIsInVuaW9uX2lkIjoib05RejQwUnFpaVdzVkQ2Q2E3N3ltQ2JZaWxuQSIsIm1vY2tfb3BlbmlkIjpmYWxzZSwibWluaV9vcGVuaWQiOiIiLCJleHAiOjE2NTA0NDM0NzYsImlhdCI6MTY1MDQzNzg3Nn0.IYrW3VVoDvvQgUaA6alvM3GamS_8OKFhD4TCSy2aInk");
            headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63060012)");
            headers.Add("x-uuid", "3E4BBCBF68FF8B3382B2DC3F0B51387E");
            headers.Add("Content-Type", "application/json");
            headers.Add("Sec-Fetch-Site", "same-site");
            headers.Add("Sec-Fetch-Mode", "cors");
            headers.Add("Sec-Fetch-Dest", "empty");
            headers.Add("Referer", "https://appv2.ynhdkc.com/registration_order_confirm");
            headers.Add("Accept-Encoding", "gzip, deflate, br");
            headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            var taskResponse = HttpClientVaccine.PostHttpObjectAsync(url, stringContent);
            var stringResponse = await taskResponse;

        }
    }
}
