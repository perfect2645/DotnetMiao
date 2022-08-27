using Baohe.appointment;
using Baohe.search.ArrangeWater;
using Baohe.search.auth;
using Baohe.search.doctor;
using Baohe.search.numbers;
using Baohe.search.user;
using Baohe.session;
using Base.logging;
using Base.viewModel;
using CoreControl.LogConsole;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
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
    internal enum SearchStatus
    {
        Start,
        WaterGet,
        NumbersGet,
    }
    internal class SearchController : HttpClientBase
    {
        private SearchStatus SearchStatus { get; set; }

        public Timer AutoRunTimer { get; set; }

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            InitAuoRunTimer();
        }

        private void InitAuoRunTimer()
        {
            AutoRunTimer = new Timer();
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
        }

        private async void AutoRunTimer_ElapsedAsync(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (SearchStatus == SearchStatus.NumbersGet)
                {
                    AutoRunTimer.Stop();
                    BuildMiaoOrder();
                }
                await SearchMiaoInfo();
            }
            catch (HttpException ex)
            {
                BaoheSession.PrintLogEvent.Publish(this, ex.Message);
            }
            catch (Exception ex)
            {
                BaoheSession.PrintLogEvent.Publish(this, ex.StackTrace ?? ex.Message);
            }
        }

        #endregion AutoRun



        private async Task SearchMiaoInfo()
        {
            if (SearchStatus == SearchStatus.Start)
            {
                var arrangeWater = HttpServiceController.GetService<ArrangeWaterController>();
                var isWaterGet = await arrangeWater.GetArrangeWaterAsync(true);
                if (isWaterGet)
                {
                    SearchStatus = SearchStatus.WaterGet;
                }
            }

            if (SearchStatus == SearchStatus.WaterGet)
            {
                var appointNumbers = HttpServiceController.GetService<AppointNumbersController>();
                var isNumbersGet = await appointNumbers.GetNumbersAsync(true);
                if (isNumbersGet)
                {
                    SearchStatus = SearchStatus.NumbersGet;
                }
            }
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
                for(var i = 0; i < 1; i++)
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
