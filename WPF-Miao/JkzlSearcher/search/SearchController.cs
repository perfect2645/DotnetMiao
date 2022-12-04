using HttpProcessor.Container;
using JkzlSearcher.auth;
using JkzlSearcher.search.user;
using JkzlSearcher.session;
using System;
using System.Threading.Tasks;

namespace JkzlSearcher.search
{
    internal class SearchController
    {
        private HosDeptController _deptController;

        public SearchController()
        {
            _deptController = HttpServiceController.GetService<HosDeptController>();
        }

        public void SearchByHospitalId()
        {
            var hospitalId = MainSession.GetNextHospitalId();
            _deptController.GetHosDeptAsync(hospitalId);
        }

        internal async void CheckAuthAsync()
        {
            var userController = HttpServiceController.GetService<UserInfoController>();
            await userController.GetUserInfoAsync();

            var authController = HttpServiceController.GetService<DoctorAuthController>();
            await authController.GetAutholicyAsync();
        }
    }
}
