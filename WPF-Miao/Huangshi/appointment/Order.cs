using Huangshi.login;
using System.Text;

namespace Huangshi.appointment
{
    public class Order
    {
        public string UserId { get; set; }
        public string UserName { get; set; } //XM 体检人姓名
        public string TCSL { get; set; } = "1";
        public string ContactPhone { get; set; } //lXDH 联系人电话
        public string Date { get; set; }  //YYSJ
        public string Time { get; set; } //YYSJD
        public string HYZT { get; set; } = "0"; // 婚姻状态
        public string ZJLX { get; set; } = "1"; // 证件类型
        public string SFZHM { get; set; } // SFZHM 身份证号码
        public string UserPhone { get; set; } // 就诊人手机号码
        public string XB { get; set; } = "0";
        public string Birthday { get; set; } // CSRQ 出生日期
        public string addItem { get; set; } // 加项折扣
        public string addItemExt { get; set; } // 加项折扣
        public string zffs { get; set; } = "0"; // 支付方式
        public string tjkmm { get; set; } // 会员卡密码
        public string zglb { get; set; } = "0"; // 资格类别
        public string xklb { get; set; } = "0"; // 学科类别
        public string dw { get; set; } = "0"; // 单位选择
        public string yzm { get; set; }
        public string zyjxZK { get; set; } = "1";
        internal HuangshiLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
