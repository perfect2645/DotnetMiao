using Dalian.login;
using Dalian.session;
using HttpProcessor.Content;
using Utils;
using Utils.datetime;

namespace Dalian.common
{
    internal class DalianContent : HttpStringContent
    {
        public DalianLogin User { get; private set; }

        public DalianContent(string baseUrl, DalianLogin user) : base(baseUrl)
        {
            User = user;
            var timestamp = DateTimeUtil.GetTimeStamp();
            RequestUrl = $"{baseUrl}{timestamp}";
        }

        protected virtual void BuildHeader()
        {
            AddHeader("Host", "www.114yygh.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("Origin", "https://www.114yygh.com");
            AddHeader("Request-Source", "WE_CHAT");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");

            AddHeader("Cookie", $"{User.Session} imed_session_tm={User.SessionTime}");
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");

            //var hosCode = MainSession.PlatformSession.GetString(Constants.HospitalId);
            //var firstDept = MainSession.PlatformSession.GetString(Constants.FirstDeptCode);
            //var secondDept = MainSession.PlatformSession.GetString(Constants.DeptId);
            //AddHeader("Referer", $"https://www.114yygh.com/wechat/hospital/submission?hosCode={hosCode}&firstDeptCode={firstDept}&secondDeptCode={secondDept}&target=2023-06-07&uniqProductKey=d1df4e7ea2a52c67dd66aa07ef5dee19b79d358e&dutyTime=202306071302-202306071626");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
