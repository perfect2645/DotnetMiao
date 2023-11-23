using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;
using Zhuzher.collectsun;
using Zhuzher.Exchange;
using Zhuzher.miaosha;
using Zhuzher.Play;
using Zhuzher.Score;

namespace Zhuzher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitControllerAsync();
        }

        private void InitControllerAsync()
        {
            Task.Factory.StartNew(() =>
            {
                InitController();
            });
        }
        private void InitController()
        {
            HttpServiceController.AddTransientService<CollectSunController>();
            HttpServiceController.AddTransientService<CollectSunV2Controller>();
            HttpServiceController.AddTransientService<JoinTeamController>();
            HttpServiceController.AddTransientService<ExchangeController>();
            HttpServiceController.AddTransientService<SeckillController>();
            HttpServiceController.AddTransientService<JifenSurkillController>();
            HttpServiceController.AddTransientService<ScoreKillController>();
            HttpServiceController.AddTransientService<PlayController>();
            HttpServiceController.AddTransientService<PlayRewardController>();
            HttpServiceController.AddTransientService<CollectScoreController>();
            HttpServiceController.AddTransientService<UnCollectedScoreController>();
            HttpServiceController.AddTransientService<ScoreBetController>();
            HttpServiceController.AddTransientService<GoodDetailController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
