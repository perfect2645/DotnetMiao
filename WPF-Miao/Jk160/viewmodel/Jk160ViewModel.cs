using Base.Events;
using Base.viewModel;
using Base.viewModel.hospital;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Jk160.constants;
using Jk160.search;
using Jk160.session;
using Jk160.viewmodel.platform;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Jk160.viewModel
{
    internal class Jk160ViewModel: ViewModelBase
    {
        #region Properties

        public ICommand AppointmentCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand AutoRunCommand { get; set; }

        #endregion Properties

        #region Constructor

        public Jk160ViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitStaticData();
            InitCommands();
            Jk160Session.PrintLogEvent = PrintLogEvent;
        }

        private void InitStaticData()
        {
            Departments = new List<HospitalDept>();
            Departments.Add(new Jk160Dept("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山区经开区井岗镇社区卫生服务中心",
                "7229244", "四价Hpv"));
            Departments.Add(new Jk160Dept("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山区经开区井岗镇社区卫生服务中心",
                "7209050", "(测试)儿童保健科"));


            InitPlatformSession();
        }

        private void InitPlatformSession()
        {
            try
            {
                var tsStr = DateTimeUtil.GetTimeStamp();
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

        private void InitCommands()
        {
            SearchCommand = new RelayCommand(ExecuteSearchAsync, CanExecuteSearch);

            AutoRunCommand = new RelayCommand(ExecuteAutoRun);

            SessionEvents.Instance.Subscribe(LogSession);

            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);
        }

        #endregion Constructor

        #region Search

        private bool CanExecuteSearch()
        {
            return true;
        }

        private async void ExecuteSearchAsync()
        {
            try
            {
                var searchController = HttpServiceController.GetService<Jk160Search>();
                await searchController.SearchAllAsync(SessionItem);
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

        #endregion Search

        #region AutoRun

        private void ExecuteAutoRun()
        {
            Jk160Session.Cookie = SessionItem.Cookie;
            Task.Factory.StartNew(async () =>
            {
                await AutoRunAsync();
            });
        }

        private async Task AutoRunAsync()
        {
            var searchController = HttpServiceController.GetService<Jk160Search>();
            await searchController.AutoSearchAsync(SessionItem);
        }

        #endregion AutoRun

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as Jk160Dept;
            Jk160Session.PlatformSession.AddOrUpdate(Constant.PlatformType, selectedDept.PlatformId);
            Jk160Session.PlatformSession.AddOrUpdate(Constant.HospitalId, selectedDept.HospitalId);
            Jk160Session.PlatformSession.AddOrUpdate(Constant.DeptId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());

            Jk160Session.BuildMiaoSession(Jk160Session.PlatformSession[Constant.DeptId].NotNullString());
        }

        #endregion Hospital Dept

    }
}
