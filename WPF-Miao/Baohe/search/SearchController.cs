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
using System.Timers;
using Utils;

namespace Baohe.search
{
    internal class SearchController : HttpClientBase
    {

        public Timer AutoRunTimer { get; set; }

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            InitAuoRunTimer();
        }

        private void InitAuoRunTimer()
        {
            AutoRunTimer.Enabled = false;
            AutoRunTimer.Interval = 10000;

            AutoRunTimer.AutoReset = true;

            AutoRunTimer.Elapsed += new System.Timers.ElapsedEventHandler(AutoRunTimer_ElapsedAsync);
        }

        internal async Task SearchAllAsync(ISessionItem sessionItem)
        {
            await SearchUserInfo(sessionItem);

            await SearchMiaoInfo();

            //BuildMiaoOrder();
        }

        #region AutoRun

        internal async Task AutoSearchAsync(ISessionItem sessionItem)
        {
            await SearchUserInfo(sessionItem);

            //await SearchMiaoInfo();
            AutoRunTimer.Start();

            BuildMiaoOrder();
        }

        private async void AutoRunTimer_ElapsedAsync(object? sender, ElapsedEventArgs e)
        {
            await SearchMiaoInfo();
        }

        #endregion AutoRun



        private static async Task SearchMiaoInfo()
        {
            var arrangeWater = HttpServiceController.GetService<ArrangeWaterController>();
            await arrangeWater.GetArrangeWaterAsync(true);

            var appointNumbers = HttpServiceController.GetService<AppointNumbersController>();
            await appointNumbers.GetNumbersAsync(true);
        }

        private async Task SearchUserInfo(ISessionItem sessionItem)
        {
            var authController = HttpServiceController.GetService<AuthController>();
            authController.GetCookieAdvance(sessionItem.Cookie);

            var userInfoContr = HttpServiceController.GetService<UserInfoController>();
            await userInfoContr.GetUserInfoAsync();

            var doctorContr = HttpServiceController.GetService<DoctorController>();
            await doctorContr.GetDoctorListAsync();

            //var liudiao = HttpServiceController.GetService<LiudiaoController>();
            //await liudiao.LiudiaoAsync(sessionItem);

            BuildMemberOrder(userInfoContr.MemberList);
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
            Parallel.ForEach(orders, (order) =>
            {
                order.FillContent(BaoheSession.MiaoSession);
            });
        }
    }
}
