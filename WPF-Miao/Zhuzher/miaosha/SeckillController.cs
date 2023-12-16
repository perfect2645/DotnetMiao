using HttpProcessor.Client;
using HttpProcessor.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.timerUtil;
using Zhuzher.Exchange;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.miaosha
{
    internal class SeckillController : HttpClientBase
    {
        private Dictionary<int, IntervalOnTime> IntervalList = new Dictionary<int, IntervalOnTime>();
        private readonly UserProjectList UserProjectList = new UserProjectList();

        public SeckillController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void Seckill(List<MiaoshaItem> miaoshaList)
        {
            MainSession.PrintLogEvent?.Publish(this, $"****秒杀开始预备");
            var miaoshaGroups = miaoshaList.GroupBy(x => x.StartTime).ToList();

            foreach (var group in miaoshaGroups)
            {
                foreach(var item in group)
                {
                    foreach(var user in UserProjectList.UserProjects)
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
    }
}
