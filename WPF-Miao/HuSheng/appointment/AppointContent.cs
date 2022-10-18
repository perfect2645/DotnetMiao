using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HuSheng.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace HuSheng.appointment
{
    internal class AppointContent : HttpStringContent
    {
        private HtmlDoc FormDoc { get; }

        public AppointContent(string url, HtmlDoc formDoc) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";

            FormDoc = formDoc;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "hoosn.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Cache-Control", "max-age=0");
            AddHeader("Upgrade-Insecure-Requests", "1");
            AddHeader("Origin", "http://hoosn.cn");
            AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }


        private void BuildContent()
        {
            HushengSession.PrintLogEvent.Publish(this, "BuildContent --- Start");
            try
            {
                BuildBasicContent();
            }
            catch(Exception ex)
            {
                throw new HttpException(ex);
            }

            //AddContent("appointmentDate", HushengSession.MiaoSession["Date"]);
            //AddContent("dateCount", HushengSession.MiaoSession["Time"]);
            //AddContent("ETID", HushengSession.MiaoSession["Etid"]);
            //AddContent("VACCINES_NAME", "18");
            //AddContent("DOC_CUSTOM_VACCINE_GUID", HushengSession.MiaoSession["GUID"]);
            //AddContent("code", HushengSession.MiaoSession["Yzm"]);
            HushengSession.PrintLogEvent.Publish(this, "BuildContent --- End");
        }

        private void BuildBasicContent()
        {
            AddFormContent("appointmentDate");
            AddFormContent("appointmentTime", true);
            AddFormContent("appointmentTimeId");
            AddFormContent("dataFrom");
            AddFormContent("dealStatus");
            AddFormContent("etId");
            AddFormContent("configValue");
            AddFormContent("openid");
            AddFormContent("subOpenid");
            AddFormContent("subWechatId");
            AddFormContent("subId");
            AddFormContent("payFlag");
        }

        private void AddFormContent(string elementId, bool isEncode = false)
        {
            var appointmentDate = FormDoc.GetFormString(elementId, isEncode);
            AddContent("appointmentDate", appointmentDate);
        }
    }
}
