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
        public ICommand FragmentHoldCommand { get; set; }
        public ICommand FragmentComposeCommand { get; set; }
        public ICommand FragmentOpenPriceCommand { get; set; }
        public ICommand PuliDrawPrizeCommand { get; set; }
        

        private int _activityGameId = 1744;
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
            FragmentHoldCommand = new RelayCommand(FragmentHold);
            FragmentComposeCommand = new RelayCommand(FragmentCompose);
            FragmentOpenPriceCommand = new RelayCommand(FragmentOpenPrice);
            PuliDrawPrizeCommand = new RelayCommand(PuliDrawPrize);
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

        private void PuliDrawPrize()
        {
            try
            {
                var puliDrawPrizeController = HttpServiceController.GetService<PuliDrawPrizeController>();
                puliDrawPrizeController.GoodGetAsync();
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

        #region Fragment

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

        private void FragmentHold()
        {
            try
            {
                var fragmentPlay = HttpServiceController.GetService<FragmentExchangeController>();
                fragmentPlay.FragmentHoldAsync(ActivityGameId);
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

        private void FragmentCompose()
        {
            try
            {
                var fragmentPlay = HttpServiceController.GetService<FragmentExchangeController>();
                fragmentPlay.FragmentComposeAsync(ActivityGameId);
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

        private void FragmentOpenPrice()
        {
            try
            {
                var fragmentPlay = HttpServiceController.GetService<FragmentExchangeController>();
                fragmentPlay.FragmentOpenPrizeAsync(ActivityGameId);
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

        #endregion Fragment
    }
}
