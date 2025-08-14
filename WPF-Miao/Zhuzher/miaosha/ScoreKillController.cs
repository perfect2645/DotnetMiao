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
    internal class ScoreKillController : HttpClientBase
    {
        private readonly UserProjectList UserProjectList = new UserProjectList();
        private Dictionary<string, ActionOnTime> OntimeList = new Dictionary<string, ActionOnTime>();

        public ScoreKillController(HttpClient httpClient) : base(httpClient)
        {
        }

        #region Seckill V3

        public void SeckillV3(List<ScoreItem> miaoshaList)
        {
            MainSession.PrintLogEvent?.Publish(this, $"****秒杀开始预备");
            var miaoshaGroups = miaoshaList.GroupBy(x => x.StartTime).ToList();

            foreach (var group in miaoshaGroups)
            {
                foreach (var user in UserProjectList.UserProjects)
                {
                    MainSession.PrintLogEvent?.Publish(this, $"准备User:{user.UserName},Group:{group.Key}");
                    var surkillHandler = HttpServiceController.GetService<JifenSurkillController>();
                    var ontime = new ActionOnTime(() => SeckillOntimeTickV3(user, group, surkillHandler), user.UserName, group.Key);
                    OntimeList.AddOrUpdate($"{group.Key}-{user.UserName}", ontime);
                }
            }

            MainSession.PrintLogEvent?.Publish(this, $"****秒杀预备结束");
        }
        public void SeckillOntimeTickV3(UserProject user, IGrouping<DateTime, ScoreItem> group, JifenSurkillController surkillHandler)
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
                        foreach (var item in group)
                        {
                            if (item.Status == 3)
                            {
                                continue;
                            }
                            MainSession.PrintLogEvent?.Publish(item, $"开始秒杀！{user.UserName}{item.Log}");
                            surkillHandler.ScoreSurkill(user, item);
                            Thread.Sleep(100);
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

        public void SeckillV2(List<ScoreItem> miaoshaList)
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
                        var exchangeHandler = HttpServiceController.GetService<JifenSurkillController>();
                        var ontime = new ActionOnTime(() => SeckillOntimeTick(user, item, exchangeHandler), item.GoodName, item.StartTime);
                        OntimeList.AddOrUpdate(item.ExchangeGoodId.NotNullString(), ontime);
                    }
                }
            }

            MainSession.PrintLogEvent?.Publish(this, $"****秒杀预备结束");
        }
        public void SeckillOntimeTick(UserProject user, ScoreItem item, JifenSurkillController exchangeHandler)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OntimeList[item.ExchangeGoodId.NotNullString()].StopTimer();
                    item.Status = 1; //开始
                    MainSession.PrintLogEvent?.Publish(item, $"开始秒杀！{user.UserName}{item.Log}");
                    var count = 0;
                    while (item.Status != 3)
                    {
                        count++;
                        Task.Delay(200);
                        exchangeHandler.ScoreSurkill(user, item);
                    }
                }
                catch (Exception ex)
                {
                    MainSession.PrintLogEvent?.Publish(this, ex.Message);
                }
            });
        }

        #endregion Seckill V2
    }
}
