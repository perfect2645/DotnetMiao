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
using System.Threading.Tasks;
using System.Windows.Input;
using Zhuzher.collectsun;
using Zhuzher.Exchange;
using Zhuzher.miaosha;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.viewmodel
{
    internal class ZhuzherViewModel : ViewModelBase
    {
        #region Properties

        public ICommand CollectSunCommand { get; set; }
        public ICommand ExchangeCommand { get; set; }
        public ICommand SeckillCommand { get; set; }
        public ICommand ScoreSeckillCommand { get; set; }
        public ICommand JoinTeamCommand { get; set; }

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
                ZhuzherSession.ActivityId = value;
                NotifyUI(() => ActivityId);
            }
        }

        #endregion Properties

        #region Constructor

        public ZhuzherViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitStaticData();
            InitCommands();
            ZhuzherSession.PrintLogEvent = PrintLogEvent;
        }

        private void InitStaticData()
        {
            var mslist = new MiaoshaItemList();
            MiaoshaList = mslist.MiaoshaList;

            ActivityId = "914";
            ZhuzherSession.InviteCode = "ADA2PV";
        }

        private void InitCommands()
        {
            CollectSunCommand = new RelayCommand(ExecuteCollectSunAsync, CanExecuteCollectSun);
            ExchangeCommand = new RelayCommand(ExecuteExchange);
            SeckillCommand = new RelayCommand(ExecuteSeckill);
            ScoreSeckillCommand = new RelayCommand(ExecuteScoreSeckill);
            JoinTeamCommand = new RelayCommand(ExecuteJoinTeam);

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
                ZhuzherSession.Cookie = Cookie;
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
                ZhuzherSession.Cookie = Cookie;
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

        #endregion Exchange

        #region Seckill

        private void ExecuteSeckill()
        {
            try
            {
                ZhuzherSession.Cookie = Cookie;
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

        private void ExecuteScoreSeckill()
        {
            try
            {
                ZhuzherSession.Cookie = Cookie;
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

        

        #endregion Seckill

        #region JoinTeam

        private void ExecuteJoinTeam()
        {
            try
            {
                ZhuzherSession.Cookie = Cookie;
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

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session
    }
}
