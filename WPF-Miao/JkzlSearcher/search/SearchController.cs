using HttpProcessor.Container;
using JkzlSearcher.auth;
using JkzlSearcher.output;
using JkzlSearcher.search.user;
using JkzlSearcher.session;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var outputController = BuildOutputController(depts, hospital);
            if (!outputController.HasDepartments())
            {
                return;
            }

            foreach (var dept in outputController.Hospital.Departments)
            {
                var doctorController = HttpServiceController.GetService<DoctorController>();
                var doctors = await doctorController.GetDoctorListAsync(hospitalId, dept.Key);
                if (!doctors.HasItem())
                {
                    continue;
                }
                outputController.AddDoctors(doctors);
            }

            outputController.SaveHospital();
        }

        private OutputController BuildOutputController(List<Dictionary<string, object>> depts, 
            Dictionary<string, object> hospital)
        {
            return new OutputController(depts, hospital);
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
