using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Utils;
using Zhuzher.collectsun;
using Zhuzher.Common;
using Zhuzher.Play;
using Zhuzher.Post;
using Zhuzher.search;
using Zhuzher.session;
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
        public ICommand InviteHelpCommand { get; set; }

        private int _activityGameId = 1756;
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

        private List<UserProject> _targetHelpUserList;
        public List<UserProject> TargetHelpUserList
        {
            get { return _targetHelpUserList; }
            set
            {
                _targetHelpUserList = value;
                NotifyUI(() => TargetHelpUserList);
            }
        }

        private UserProject _targetHelpUser;
        public UserProject TargetHelpUser
        {
            get { return _targetHelpUser; }
            set
            {
                _targetHelpUser = value;
                NotifyUI(() => TargetHelpUser);
                UpdateSelectedSource();
            }
        }

        private List<UserProject> _sourceHelpUserList;
        public List<UserProject> SourceHelpUserList
        {
            get { return _sourceHelpUserList; }
            set
            {
                _sourceHelpUserList = value;
                NotifyUI(() => SourceHelpUserList);
            }
        }

        private List<UserProject> _selectedHelpUserList;
        public List<UserProject> SelectedHelpUserList
        {
            get { return _selectedHelpUserList; }
            set
            {
                _selectedHelpUserList = value;
                NotifyUI(() => SelectedHelpUserList);
            }
        }

        private int? _helpCount;

        public int? HelpCount
        {
            get { return _helpCount; }
            set
            {
                _helpCount = value;
                NotifyUI(() => HelpCount);
                UpdateSelectedSource();
            }
        }

        #endregion Properties

        private void InitActivityTab()
        {
            TargetHelpUserList = MainSession.UserProjectList.UserProjects.ToList();
            SourceHelpUserList = MainSession.UserProjectList.UserProjects.ToList();
            SelectedHelpUserList = new List<UserProject>();
            TargetHelpUser = TargetHelpUserList.FirstOrDefault();

            ActivityChanceCommand = new RelayCommand(ExecuteActivityChance);
            GuessResultCommand = new RelayCommand(ExecuteGuessResult);
            FragmentExchangeCommand = new RelayCommand(FragmentExchange);
            FragmentLotteryCommand = new RelayCommand(FragmentLottery);
            FragmentHoldCommand = new RelayCommand(FragmentHold);
            FragmentComposeCommand = new RelayCommand(FragmentCompose);
            FragmentOpenPriceCommand = new RelayCommand(FragmentOpenPrice);
            PuliDrawPrizeCommand = new RelayCommand(PuliDrawPrize);
            InviteHelpCommand = new RelayCommand(InviteHelp);
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

        private void InviteHelp()
        {
            try
            {
                var puliDrawPrizeController = HttpServiceController.GetService<InviteHelpController>();
                puliDrawPrizeController.InviteHelpAsync(TargetHelpUser, SelectedHelpUserList);
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

        #region Invite

        private void UpdateSelectedSource()
        {
            if (TargetHelpUser == null)
            {
                return;
            }

            if (!SourceHelpUserList.HasItem())
            {
                return;
            }

            var usersExceptTarget = SourceHelpUserList.Where(x => x.UserId != TargetHelpUser.UserId).ToList();
            if (HelpCount == null || HelpCount <= 0 || HelpCount >= SourceHelpUserList.Count)
            {
                SelectedHelpUserList = usersExceptTarget;
                return;
            }

            var targetUserIndex = SourceHelpUserList.IndexOf(TargetHelpUser);
            if (targetUserIndex == -1)
            {
                MainSession.PrintLogEvent.Publish(this, "TargetHelpUserChanged error!");
                SelectedHelpUserList = new List<UserProject>();
                return;
            }

            SelectedHelpUserList = SourceHelpUserList.Skip(targetUserIndex + 1).Take(HelpCount.Value).ToList();
        }

        #endregion Invite
    }
}
