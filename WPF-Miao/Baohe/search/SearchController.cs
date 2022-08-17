using Baohe.search.ArrangeWater;
using Baohe.search.auth;
using Baohe.search.doctor;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Container;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baohe.search
{
    internal class SearchController : HttpClientBase
    {
        public SearchController(HttpClient httpClient) : base(httpClient)
        {

            
        }

        internal async Task SearchAllAsync(ISessionItem sessionItem)
        {
            var authController = HttpServiceController.GetService<AuthController>();
            authController.GetCookieAdvance(sessionItem.Cookie);

            var userInfoContr = HttpServiceController.GetService<UserInfoController>();
            await userInfoContr.GetUserInfoAsync(sessionItem);

            var doctorContr = HttpServiceController.GetService<DoctorController>();
            await doctorContr.GetDoctorListAsync(sessionItem);

            var arrangeWater = HttpServiceController.GetService<ArrangeWaterController>();
            await arrangeWater.GetArrangeWaterAsync(sessionItem);

            var appointNumbers = HttpServiceController.GetService<AppointNumbersController>();
            await appointNumbers.GetNumbersAsync(sessionItem);
        }
    }
}
