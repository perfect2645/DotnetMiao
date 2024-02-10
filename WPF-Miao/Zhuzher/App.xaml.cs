using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;
using Zhuzher.collectsun;
using Zhuzher.Exchange;
using Zhuzher.miaosha;
using Zhuzher.Play;
using Zhuzher.Post;
using Zhuzher.Score;
using Zhuzher.vote;

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
            HttpServiceController.AddTransientService<CollectSunV3Controller>();
            HttpServiceController.AddTransientService<JoinTeamController>();
            HttpServiceController.AddTransientService<ExchangeController>();
            HttpServiceController.AddTransientService<SeckillController>();
            HttpServiceController.AddTransientService<JifenSurkillController>();
            HttpServiceController.AddTransientService<ScoreKillController>();
            HttpServiceController.AddTransientService<PlayController>();
            HttpServiceController.AddTransientService<ScorePlayController>();
            HttpServiceController.AddTransientService<PlayRewardController>();
            HttpServiceController.AddTransientService<CollectScoreController>();
            HttpServiceController.AddTransientService<UnCollectedScoreController>();
            HttpServiceController.AddTransientService<ScoreBetController>();
            HttpServiceController.AddTransientService<GoodDetailController>();
            HttpServiceController.AddTransientService<GuseeBetController>();
            HttpServiceController.AddTransientService<PlayDetailController>();
            HttpServiceController.AddTransientService<ScoreExchangeController>();
            HttpServiceController.AddTransientService<LootController>();
            HttpServiceController.AddTransientService<VoteController>();
            HttpServiceController.AddTransientService<CommentController>();
            HttpServiceController.AddTransientService<LikeController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
