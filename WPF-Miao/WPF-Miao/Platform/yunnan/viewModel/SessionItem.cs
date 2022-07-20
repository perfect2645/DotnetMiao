using HttpProcessor.Container;
using System;
using System.Threading.Tasks;
using Utils;
using WPF_Miao.Platform.yunnan.getUserInfo;
using WPF_Miao.Platform.yunnan.session;

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
                if (_cookie == value)
                {
                    return;
                }
                _cookie = value;
                TryGetUserInfo();
                NotifyUI(() => Cookie);
            }
        }

        private string _referer;

        public string Referer
        {
            get { return _referer; }
            set 
            {
                if (_referer == value)
                {
                    return;
                }
                _referer = value;
                TryGetUserInfo();
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

        #region User Session

        public Action GetUserSessionAction { get; set; }

        public SessionItem()
        {
            YunnanSession.AddOrUpdate(this);
            GetUserSessionAction = new Action(GetUserSessionAsync);
        }

        private void GetUserSessionAsync()
        {
            var getUserTask = Task.Factory.StartNew(() =>
            {
                var controller = HttpServiceController.GetService<UserInfoController>();
                return controller.GetUserInfo().Result;
            });
            var userInfoResponse = getUserTask.Result;
        }

        private void TryGetUserInfo()
        {
            if (string.IsNullOrWhiteSpace(Cookie))
            {
                return;
            }
            if (string.IsNullOrEmpty(Referer))
            {
                return;
            }

            GetUserSessionAction?.Invoke();
        }

        #endregion User Session
    }
}
