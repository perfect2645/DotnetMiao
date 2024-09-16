using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Windows.Input;
using Zhuzher.Common;
using Zhuzher.Play;
using Zhuzher.Post;
using Zhuzher.vote;

namespace Zhuzher.viewmodel
{
    internal partial class ZhuzherViewModel
    {
        #region Properties
        public ICommand ActivityChanceCommand { get; set; }
        public ICommand GuessResultCommand { get; set; }
        public ICommand FragmentExchangeCommand { get; set; }
        public ICommand FragmentLotteryCommand { get; set; }

        private int _activityGameId = 1685;
        public int ActivityGameId
        {
            get { return _activityGameId; }
            set
            {
                NotifyUI(() => ActivityGameId);
                _activityGameId = value;
            }
        }

        private int _goodId;
        public int GoodId
        {
            get { return _goodId; }
            set
            {
                NotifyUI(() => GoodId);
                _goodId = value;
            }
        }

        #endregion Properties

        private void InitActivityComments()
        {
            ActivityChanceCommand = new RelayCommand(ExecuteActivityChance);
            GuessResultCommand = new RelayCommand(ExecuteGuessResult);
            FragmentExchangeCommand = new RelayCommand(FragmentExchange);
            FragmentLotteryCommand = new RelayCommand(FragmentLottery);
        }

        private void ExecuteActivityChance()
        {
            try
            {
                var chanceController = HttpServiceController.GetService<PlayChanceController>();
                chanceController.GetChanceAsync();
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

        private void ExecuteGuessResult()
        {
            try
            {
                var myGuessController = HttpServiceController.GetService<MyGuessController>();
                myGuessController.GetGuessResultAsync(214);
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

        private void FragmentExchange()
        {
            try
            {
                var fragmentPlay = HttpServiceController.GetService<FragmentExchangeController>();
                fragmentPlay.FragmentExchangeAsync(ActivityGameId, GoodId);
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

        private void FragmentLottery()
        {
            try
            {
                var fragmentPlay = HttpServiceController.GetService<FragmentExchangeController>();
                fragmentPlay.FragmentLotteryAsync(ActivityGameId);
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
    }
}
