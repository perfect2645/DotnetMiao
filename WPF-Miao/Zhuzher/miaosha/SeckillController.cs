using HttpProcessor.Client;
using HttpProcessor.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public SeckillController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void Seckill(List<MiaoshaItem> miaoshaList)
        {
            ZhuzherSession.PrintLogEvent?.Publish(this, $"****秒杀开始预备");
            var miaoshaGroups = miaoshaList.GroupBy(x => x.StartTime).ToList();

            foreach (var group in miaoshaGroups)
            {
                foreach(var item in group)
                {
                    var exchangeHandler = HttpServiceController.GetService<ExchangeController>();
                    var interval = new IntervalOnTime(() => SeckillTick(item, exchangeHandler), item.GoodName, group.Key);
                    IntervalList.AddOrUpdate(item.GameGoodId, interval);
                }
            }

            ZhuzherSession.PrintLogEvent?.Publish(this, $"****秒杀预备结束");
        }

        public void SeckillTick(MiaoshaItem item, ExchangeController exchangeHandler)
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
                    ZhuzherSession.PrintLogEvent?.Publish(item, $"开始秒杀！{item.Log}");
                    exchangeHandler.Seckill(item);
                }
                catch (Exception ex)
                {
                    ZhuzherSession.PrintLogEvent?.Publish(this, ex.Message);
                }
            });
        }
    }
}
