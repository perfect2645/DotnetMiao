using Base.Events;
using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Zhuzher.collectsun;
using Zhuzher.Exchange;
using Zhuzher.miaosha;
using Zhuzher.Play;
using Zhuzher.Score;
using Zhuzher.search;
using Zhuzher.session;
using Zhuzher.vote;

namespace Zhuzher.viewmodel
{
    internal partial class ZhuzherViewModel : ViewModelBase
    {
        #region Properties

        public ICommand CollectSunCommand { get; set; }
        public ICommand ExchangeCommand { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand SeckillCommand { get; set; }
        public ICommand ScoreSeckillCommand { get; set; }
        public ICommand JoinTeamCommand { get; set; }
        public ICommand CollectScoreCommand { get; set; }
        public ICommand ScoreBetCommand { get; set; }
        public ICommand GuessBetCommand { get; set; }
        public ICommand TrackPlayCommand { get; set; }
        public ICommand ScoreExchangeCommand { get; set; }
        public ICommand LootCommand { get; set; }
        public ICommand ScorePlayCommand { get; set; }
        public ICommand VoteCommand { get; set; }
        


        private List<MiaoshaItem> _miaoshaList;
        public List<MiaoshaItem> MiaoshaList
        {
            get { return _miaoshaList; }
            set
            {
                _miaoshaList = value;
                NotifyUI(() => MiaoshaList);
            }
        }

        private List<ScoreItem> _scoreMiaoshaList;
        public List<ScoreItem> ScoreMiaoshaList
        {
            get { return _scoreMiaoshaList; }
            set
            {
                _scoreMiaoshaList = value;
                NotifyUI(() => ScoreMiaoshaList);
            }
        }

        private string _activityId;
        public string ActivityId
        {
            get { return _activityId; }
            set
            {
                _activityId = value;
                MainSession.ActivityId = value;
                NotifyUI(() => ActivityId);
            }
        }

        #endregion Properties

        #region Constructor

        public ZhuzherViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitStaticData();
            InitCommands();
            MainSession.PrintLogEvent = PrintLogEvent;
        }

        private void InitStaticData()
        {
            var mslist = new MiaoshaItemList();
            MiaoshaList = mslist.MiaoshaList;

            var scorekilllist = new ScoreItemList();
            ScoreMiaoshaList = scorekilllist.MiaoshaList;

            ActivityId = "1020";
            MainSession.InviteCode = "T3L84I";
        }

        private void InitCommands()
        {
            CollectSunCommand = new RelayCommand(ExecuteCollectSunAsync, CanExecuteCollectSun);
            ExchangeCommand = new RelayCommand(ExecuteExchange);
            PlayCommand = new RelayCommand(ExecutePlay);
            SeckillCommand = new RelayCommand(ExecuteSeckill);
            ScoreSeckillCommand = new RelayCommand(ExecuteScoreSeckill);
            JoinTeamCommand = new RelayCommand(ExecuteJoinTeam);
            CollectScoreCommand = new RelayCommand(ExecuteCollectScore);
            ScoreBetCommand = new RelayCommand(ExecuteScoreBet);
            GuessBetCommand = new RelayCommand(ExecuteGuessBet);
            TrackPlayCommand = new RelayCommand(ExecuteTrackPlay);
            ScoreExchangeCommand = new RelayCommand(ExecuteScoreExchange);
            LootCommand = new RelayCommand(ExecuteLoot);
            ScorePlayCommand = new RelayCommand(ExecuteScorePlay);
            VoteCommand = new RelayCommand(ExecuteVote);
            InitPostComments();

            SessionEvents.Instance.Subscribe(LogSession);
        }

        #endregion Constructor

        #region CollectSun

        private bool CanExecuteCollectSun()
        {
            return true;
        }

        private void ExecuteCollectSunAsync()
        {
            try
            {
                MainSession.Cookie = Cookie;
                var searchController = HttpServiceController.GetService<CollectSunV2Controller>();
                searchController.CollectSunAsync();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion CollectSun

        #region Exchange

        private void ExecuteExchange()
        {
            try
            {
                MainSession.Cookie = Cookie;
                var exchangeHandler = HttpServiceController.GetService<ExchangeController>();
                exchangeHandler.ExchangeAsync();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteLoot()
        {
            try
            {
                var lootController = HttpServiceController.GetService<LootController>();
                lootController.LootAsync();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion Exchange

        #region Play

        private void ExecutePlay()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    var playHandler = HttpServiceController.GetService<PlayController>();
                    var userList = new UserProjectList();
                    foreach (var user in userList.UserProjects)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            playHandler.ActivityPlay(user);
                            Thread.Sleep(1000);
                        }
                    }
                });
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteGuessBet()
        {
            try
            {
                var betController = HttpServiceController.GetService<GuseeBetController>();

                var guessBet = new GuessBet
                {
                    ActivityId = "979",
                    ActivityGuessId = 77,
                    OptionId = 156,
                };

                betController.GuessBetAsync(guessBet);
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteTrackPlay()
        {
            try
            {
                var playController = HttpServiceController.GetService<PlayDetailController>();

                var good = new MiaoshaItem
                {
                    GameGoodId = 7985,
                    ActivityGameId = "1458",
                    GoodName = "1000元购物金"
                };

                playController.GoodAvailableAsync(good);
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion Play

        #region Seckill

        private void ExecuteSeckill()
        {
            try
            {
                MainSession.Cookie = Cookie;
                var seckillHandler = HttpServiceController.GetService<SeckillController>();
                seckillHandler.Seckill(MiaoshaList);
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }  

        #endregion Seckill

        #region JoinTeam

        private void ExecuteJoinTeam()
        {
            try
            {
                var joinTeamController = HttpServiceController.GetService<JoinTeamController>();
                joinTeamController.JoinTeamAsync();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion

        #region Score

        private void ExecuteCollectScore()
        {
            try
            {
                var scoreController = HttpServiceController.GetService<CollectScoreController>();
                scoreController.CollectScoreAsync();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteScoreBet()
        {
            try
            {
                var scoreBetController = HttpServiceController.GetService<ScoreBetController>();
                scoreBetController.ScoreBetAsync();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteScoreSeckill()
        {
            try
            {
                MainSession.Cookie = Cookie;
                var seckillHandler = HttpServiceController.GetService<ScoreKillController>();
                seckillHandler.Seckill(ScoreMiaoshaList);
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteScoreExchange()
        {
            try
            {
                MainSession.Cookie = Cookie;

                var scoreItemList = new ScoreItemList();
                Task.Factory.StartNew(() =>
                {
                    foreach (var exchangeItem in scoreItemList.ExchangeList)
                    {
                        foreach (var user in MainSession.UserProjectList.UserProjects)
                        {
                            var scoreController = HttpServiceController.GetService<ScoreExchangeController>();
                            Task.Factory.StartNew(() => scoreController.ScoreExchange(user, exchangeItem));
                        }
                        Thread.Sleep(300);
                    }
                });
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteScorePlay()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    var playHandler = HttpServiceController.GetService<ScorePlayController>();
                    var userList = new UserProjectList();
                    foreach (var user in userList.UserProjects)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            playHandler.ActivityPlay(user);
                            Thread.Sleep(1000);
                        }
                    }
                });
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion Score

        #region Vote

        private void ExecuteVote()
        {
            try
            {
                var voteController = HttpServiceController.GetService<VoteController>();
                voteController.VoteAsync();
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion Vote

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session
    }
}
