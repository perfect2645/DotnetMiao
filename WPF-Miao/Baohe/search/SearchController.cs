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
using HttpProcessor.ExceptionManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Utils;
using Utils.datetime;
using Timer = System.Timers.Timer;

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

        private readonly object OrderLock = new object();

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            InitAuoRunTimer();
        }

        private void InitAuoRunTimer()
        {
            AutoRunTimer = new Timer();
            AutoRunTimer.Enabled = false;
            AutoRunTimer.Interval = 3000;

            AutoRunTimer.AutoReset = true;

            AutoRunTimer.Elapsed += new ElapsedEventHandler(AutoRunTimer_ElapsedAsync);
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
                    BaoheSession.PrintLogEvent.Publish(this, $"AutoRunTimer stopped start. SearchStatus={SearchStatus}, Time = {DateTimeUtil.GetNow()}");
                    AutoRunTimer.Stop();

                    return;
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
                var isWaterGet = await arrangeWater.GetArrangeWaterAsync();
                if (isWaterGet)
                {
                    SearchStatus = SearchStatus.WaterGet;
                }
            }

            if (SearchStatus == SearchStatus.WaterGet)
            {
                var appointNumbers = HttpServiceController.GetService<AppointNumbersController>();
                var isNumbersGet = await appointNumbers.GetNumbersAsync();
                BaoheSession.PrintLogEvent.Publish(this, $"isNumbersGet={isNumbersGet}, Time = {DateTimeUtil.GetNow()}");
                lock (OrderLock)
                {
                    if (isNumbersGet && SearchStatus == SearchStatus.WaterGet)
                    {
                        SearchStatus = SearchStatus.NumbersGet;

                        lock (OrderLock)
                        {
                            BuildMiaoOrder();
                        }
                    }
                }
            }
        }

        private async Task SearchUserInfo(ISessionItem sessionItem)
        {
            try
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
            catch (HttpException ex)
            {
                BaoheSession.PrintLogEvent.Publish(this, ex.Message);
            }
            catch (Exception ex)
            {
                BaoheSession.PrintLogEvent.Publish(this, ex.StackTrace ?? ex.Message);
            }

        }

        private void BuildMemberOrder(List<Dictionary<string, object>> memberList)
        {
            if (!memberList.HasItem())
            {
                return;
            }

            foreach (var member in memberList)
            {
                for(var i = 0; i < 10; i++)
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

            var appContr = HttpServiceController.GetService<AppointmentController>();
            if (orders.Count > numberCount)
            {
                orders = orders.Take(numberCount).ToList();
            }

            foreach (var order in orders)
            {
                order.FillContent(BaoheSession.MiaoSession);
                Thread.Sleep(3000);
            }
        }
    }
}
