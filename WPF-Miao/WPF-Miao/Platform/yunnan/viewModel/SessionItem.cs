using HttpProcessor.Container;
using System;
using Utils;
using WPF_Miao.Platform.yunnan.getUserInfo;

namespace WPF_Miao.Platform.yunnan.viewModel
{
    public class SessionItem : NotifyChanged, ISessionItem
    {
        private string _cookie;
        public string Cookie 
        { 
            get { return _cookie; }
            set
            {
                _cookie = value;
                NotifyUI(() => Cookie);
            }
        }

        private string _referer;

        public string Referer
        {
            get { return _referer; }
            set 
            {
                _referer = value;
                NotifyUI(() => Referer);
            }
        }

        private string _userName = "userName";
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyUI(() => UserName);
            }
        }

        public int Tel { get; set; }
        public Action GetUserSessionAction { get; set; }

        public SessionItem()
        {
            GetUserSessionAction = new Action(GetUserSession);
        }

        private void GetUserSession()
        {
            var controller = HttpServiceController.GetService<UserInfoController>();
            var userInfo = controller.GetUserInfo(coo).Wait();
        }
    }
}
