using Base.Events;
using System;
using System.Text;

namespace hys020.appointment
{
    public class Order
    {
        //POST http://www.hys020.com/home/attSave_42CB58972CD44CD9945775814C00CA41_D4BEB657DDCD46F585DB5E7046996B3B_1_1_?regPhase=10%3A30%20-%2011%3A00&wechatid=gh_868741944de3&patBindId=7B28B513877B4258A45949146B98F2D6 HTTP/1.1
        public string DepartmentId { get; set; }
        public string AttId { get; set; }
        public string TimeRangeEncode { get; set; }
        public string WechatId { get; set; }
        public string PatBindId { get; set; }
        public string OrgId { get; set; }

        public string OrderUrl 
        { 
            get { return BuildUrl(); }
        }

        private string BuildUrl()
        {
            var urlHead = "http://www.hys020.com/home/attSave_";
            var url = $"{urlHead}_{DepartmentId}_{AttId}_1_1_?regPhase={TimeRangeEncode}&wechatid={WechatId}&patBindId={PatBindId}";
            return url;
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"WechatId - {WechatId}");
            sb.AppendLine($"Time - {TimeRangeEncode}");
            sb.AppendLine($"PatBindId - {PatBindId}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
