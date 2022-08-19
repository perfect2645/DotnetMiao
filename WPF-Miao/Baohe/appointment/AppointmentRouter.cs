using Baohe.search.ArrangeWater;
using Baohe.search.numbers;
using Baohe.search.varifycode;
using Base.viewModel;
using HttpProcessor.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Baohe.appointment
{
    internal class AppointmentRouter
    {
        private ISessionItem SessionItem { get; }

        public AppointmentRouter(ISessionItem sessionItem)
        {
            SessionItem = sessionItem;

            AppTimer = new System.Timers.Timer();
            AppTimer.Enabled = false;
            AppTimer.Interval = 10000;

            AppTimer.AutoReset = true;

            AppTimer.Elapsed += new System.Timers.ElapsedEventHandler(AppTimer_Elapsed);
        }

        private void AppTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            AppointTickAsync();
        }

        public Timer AppTimer { get; set; }

        public void AppointTickAsync()
        {
            Task.Factory.StartNew(async () =>
            {
                var arrangeWater = HttpServiceController.GetService<ArrangeWaterController>();
                await arrangeWater.GetArrangeWaterAsync(SessionItem);

                var appointNumbers = HttpServiceController.GetService<AppointNumbersController>();
                await appointNumbers.GetNumbersAsync(SessionItem);

                //var getverifyCode = HttpServiceController.GetService<GetVerifyCodeController>();
                //await getverifyCode.GetVerifyCodeAsync(SessionItem);

                var appContr = HttpServiceController.GetService<AppointmentController>();

                try
                {
                    appContr.AppointmentAsync(SessionItem).Wait();
                }
                catch (AggregateException ex)
                {
                    Logging.GLog.Logger.Error(ex);
                }
            });
        }
    }
}
