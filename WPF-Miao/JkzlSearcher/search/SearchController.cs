using HttpProcessor.Container;
using JkzlSearcher.auth;
using JkzlSearcher.output;
using JkzlSearcher.search.user;
using JkzlSearcher.session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace JkzlSearcher.search
{
    internal class SearchController
    {
        private HosDeptController _deptController;

        public SearchController()
        {
            _deptController = HttpServiceController.GetService<HosDeptController>();
        }

        public void SearchByHospitalIdAsync()
        {
            Task.Factory.StartNew(async () =>
            {
                await SearchByHospitalId();
            });
        }

        public async Task SearchByHospitalId()
        {
            var hospitalId = MainSession.GetNextHospitalId();
            var depts = await _deptController.GetHosDeptAsync(hospitalId);
            if (!depts.HasItem())
            {
                return;
            }

            var hospitalController = HttpServiceController.GetService<HospitalController>();
            var hospital = await hospitalController.GetHospitalByIdAsync(hospitalId);
            if (!hospital.HasItem())
            {
                return;
            }

            SaveHospital(depts, hospital);
        }

        private void SaveHospital(List<Dictionary<string, object>> depts, Dictionary<string, object> hospital)
        {
            var outputController = new OutputController(depts, hospital);
            var jsonHospital = outputController.ToHospitalJsoin();
            outputController.SaveHospital(jsonHospital);
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
