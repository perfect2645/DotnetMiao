using System.Collections.Generic;

namespace Shengzhi.login
{
    internal class ShengzhiLogin
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string AppointSource { get; set; }
        public string AppUuid { get; set; }
        public string ChannelId { get; set; }
        public string GroupCode { get; set; }
        public string HospitalWxOpenId { get; set; }
        public string ImeiId { get; set; }
        public string LoginFlag { get; set; }
        public string Password { get; set; }
        public string PhoneOperationSys { get; set; }
        public string PhoneType { get; set; }
        public string PhoneVersionNum { get; set; }
        public string PublicServiceType { get; set; }
        public string Token { get; set; }
        public string UserCode { get; set; }
        public string UserId { get; set; }
        public string UserVsId { get; set; }
        public string ForceSatification { get; set; }
        public string HospitalID { get; set; }
        public string IsAutoPwdLogin { get; set; }
        public string Loc { get; set; }
        public string Op { get; set; }
        public string OpVersion { get; set; }
        public string OperateUserSource { get; set; }
        public string UserSource { get; set; }
        public string TimeStamp { get; set; }
        public Dictionary<string, object> LoginInfo { get; set; }

        public string PatientId { get; set; }
    }
}
