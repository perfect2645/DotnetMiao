using HttpProcessor.Client;
using HttpProcessor.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;
using Utils.timerUtil;
using Zhuzher.Exchange;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.miaosha
{
    internal class SeckillController : HttpClientBase
    {
        [Obsolete]
        private Dictionary<int, IntervalOnTime> IntervalList = new Dictionary<int, IntervalOnTime>();
        private Dictionary<string, ActionOnTime> OntimeList = new Dictionary<string, ActionOnTime>();
        private readonly UserProjectList UserProjectList = new UserProjectList();

        public SeckillController(HttpClient httpClient) : base(httpClient)
        {
        }

        #region Seckill V3

        public void SeckillV3(List<MiaoshaItem> miaoshaList)
        {
            MainSession.PrintLogEvent?.Publish(this, $"****秒杀开始预备");
            var miaoshaGroups = miaoshaList.GroupBy(x => x.StartTime).ToList();

            foreach (var group in miaoshaGroups)
            {
                foreach (var user in UserProjectList.UserProjects)
                {
                    MainSession.PrintLogEvent?.Publish(this, $"准备User:{user.UserName},Group:{group.Key}");
                    var exchangeHandler = HttpServiceController.GetService<ExchangeController>();
                    var ontime = new ActionOnTime(() => SeckillOntimeTickV3(user, group, exchangeHandler), user.UserName, group.Key);
                    OntimeList.AddOrUpdate($"{group.Key}-{user.UserName}", ontime);
                }
            }

            MainSession.PrintLogEvent?.Publish(this, $"****秒杀预备结束");
        }
        public void SeckillOntimeTickV3(UserProject user, IGrouping<DateTime, MiaoshaItem> group, ExchangeController exchangeHandler)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OntimeList[$"{group.Key}-{user.UserName}"].StopTimer();
                    var allDone = false;
                    var count = 0;

                    while (!allDone && count < 1)
                    {
                        allDone = group.All(x => x.Status == 3);
                        count++;
                        foreach(var item in group)
                        {
                            if (item.Status == 3)
                            {
                                continue;
                            }
                            MainSession.PrintLogEvent?.Publish(item, $"开始秒杀！{user.UserName}{item.Log}");
                            exchangeHandler.Seckill(user, item);
                            Thread.Sleep(2000);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MainSession.PrintLogEvent?.Publish(this, ex.Message);
                }
            });
        }

        #endregion Seckill V3

        #region Seckill V2

        public void SeckillV2(List<MiaoshaItem> miaoshaList)
        {
            MainSession.PrintLogEvent?.Publish(this, $"****秒杀开始预备");
            var miaoshaGroups = miaoshaList.GroupBy(x => x.StartTime).ToList();

            foreach (var group in miaoshaGroups)
            {
                foreach (var item in group)
                {
                    foreach (var user in UserProjectList.UserProjects)
                    {
                        MainSession.PrintLogEvent?.Publish(this, $"准备User:{user.UserName}Item:{item.GoodName}");
                        var exchangeHandler = HttpServiceController.GetService<ExchangeController>();
                        var ontime = new ActionOnTime(() => SeckillOntimeTick(user, item, exchangeHandler), item.GoodName, item.StartTime);
                        OntimeList.AddOrUpdate(item.GameGoodId.NotNullString(), ontime);
                    }
                }
            }

            MainSession.PrintLogEvent?.Publish(this, $"****秒杀预备结束");
        }
        public void SeckillOntimeTick(UserProject user, MiaoshaItem item, ExchangeController exchangeHandler)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OntimeList[item.GameGoodId.NotNullString()].StopTimer();
                    item.Status = 1; //开始
                    MainSession.PrintLogEvent?.Publish(item, $"开始秒杀！{user.UserName}{item.Log}");
                    var count = 0;
                    while (item.Status != 3)
                    {
                        count ++;
                        Thread.Sleep(200);
                        exchangeHandler.Seckill(user, item);
                    }
                }
                catch (Exception ex)
                {
                    MainSession.PrintLogEvent?.Publish(this, ex.Message);
                }
            });
        }

        #endregion Seckill V2

        #region Seckill V1

        [Obsolete]
        public void Seckill(List<MiaoshaItem> miaoshaList)
        {
            MainSession.PrintLogEvent?.Publish(this, $"****秒杀开始预备");
            var miaoshaGroups = miaoshaList.GroupBy(x => x.StartTime).ToList();

            foreach (var group in miaoshaGroups)
            {
                foreach (var item in group)
                {
                    foreach (var user in UserProjectList.UserProjects)
                    {
                        MainSession.PrintLogEvent?.Publish(this, $"准备User:{user.UserName}Item:{item.GoodName}");
                        var exchangeHandler = HttpServiceController.GetService<ExchangeController>();
                        var interval = new IntervalOnTime(() => SeckillTick(user, item, exchangeHandler), item.GoodName, group.Key, 1000);
                        IntervalList.AddOrUpdate(item.GameGoodId, interval);
                    }
                }
            }

            MainSession.PrintLogEvent?.Publish(this, $"****秒杀预备结束");
        }

        [Obsolete]
        public void SeckillTick(UserProject user, MiaoshaItem item, ExchangeController exchangeHandler)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (item.Status > 1)
                    {
                        IntervalList[item.GameGoodId].StopInterval();
                    }
                    item.Status = 1; //开始
                    MainSession.PrintLogEvent?.Publish(item, $"开始秒杀！{user.UserName}{item.Log}");
                    exchangeHandler.Seckill(user, item);
                }
                catch (Exception ex)
                {
                    MainSession.PrintLogEvent?.Publish(this, ex.Message);
                }
            });
        }

        #endregion Seckill V1
    }
}
