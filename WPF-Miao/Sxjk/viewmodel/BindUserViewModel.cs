using HttpProcessor.Container;
using Sxjk.login;
using Sxjk.session;
using Sxjk.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sxjk.viewmodel
{
    internal partial class SxjkViewModel
    {
        public ICommand BindUserCommand { get; set; }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                NotifyUI(() => UserId);
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyUI(() => UserName);
            }
        }

        private string _userPhone;
        public string UserPhone
        {
            get { return _userPhone; }
            set
            {
                _userPhone = value;
                NotifyUI(() => UserPhone);
            }
        }

        private string _userBirthday;
        public string UserBirthday
        {
            get { return _userBirthday; }
            set
            {
                _userBirthday = value;
                NotifyUI(() => UserBirthday);
            }
        }

        #region Bind User

        private void BindUser()
        {
            var bindUserController = HttpServiceController.GetService<BindUserController>();
            var defaultUser = MainSession.Users.FirstOrDefault();

            var bindUser = new SxjkLogin()
            {
                Appid = defaultUser.Appid,
                BactId = defaultUser.BactId,
                Cookie = defaultUser.Cookie,
                LoginUserName = defaultUser.LoginUserName,
                BirthDay = UserBirthday,
                UserName = UserName,
                Phone = UserPhone,
            };

            defaultUser.BirthDay = UserId;
            bindUserController.BindUserAsync(bindUser);

        }

        #endregion Bind User
    }
}
