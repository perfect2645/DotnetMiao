using Baohe.appointment;
using Baohe.search.ArrangeWater;
using Baohe.search.auth;
using Baohe.search.doctor;
using Baohe.search.numbers;
using Baohe.search.user;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Container;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;

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
            await userInfoContr.GetUserInfoAsync();

            var doctorContr = HttpServiceController.GetService<DoctorController>();
            await doctorContr.GetDoctorListAsync();

            BuildMemberOrder(userInfoContr.MemberList);

            //var liudiao = HttpServiceController.GetService<LiudiaoController>();
            //await liudiao.LiudiaoAsync(sessionItem);

            var arrangeWater = HttpServiceController.GetService<ArrangeWaterController>();
            await arrangeWater.GetArrangeWaterAsync(true);

            var appointNumbers = HttpServiceController.GetService<AppointNumbersController>();
            await appointNumbers.GetNumbersAsync(true);

            BuildMiaoOrder();
        }

        private void BuildMemberOrder(List<Dictionary<string, object>> memberList)
        {
            if (!memberList.HasItem())
            {
                return;
            }

            foreach (var member in memberList)
            {
                for(var i = 0; i < 100; i++)
                {
                    var order = new Order(member, i);
                    BaoheSession.OrderSession.AddOrder(order);
                }
            }
        }

        private void BuildMiaoOrder()
        {
            var orders = BaoheSession.OrderSession.GetOrders();
            var numberCount = (BaoheSession.MiaoSession["Numbers"] as IList).Count;
            if (orders.Count > numberCount)
            {
                orders = orders.Take(numberCount).ToList();
            }
            foreach (var order in orders)
            {
                order.FillContent(BaoheSession.MiaoSession);
            }
        }
    }
}
