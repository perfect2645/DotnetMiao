using HttpProcessor.Container;
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

        internal void CheckAuth()
        {
            var userController = HttpServiceController.GetService<UserInfoController>();
            userController.GetUserInfoAsync();


        }
    }
}
