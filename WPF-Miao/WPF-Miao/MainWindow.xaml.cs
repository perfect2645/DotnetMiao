using HttpProcessor.Container;
using Logging;
using Microsoft.Extensions.DependencyInjection;
using Prism.Regions;
using System;
using System.Net.Http;
using System.Windows;
using WPF_Miao.Platform;
using WPF_Miao.Platform.DianYiTong.Hospital;
using WPF_Miao.Platform.Submit;

namespace WPF_Miao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRegionManager _regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            InitPlatform();
            _regionManager = regionManager;

            RegionManager.SetRegionName(logCtrlContent, "LogControl");
        }

        private void InitPlatform()
        {
            new DianYiTongMain();
        }

        private void Hospital_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GLog.Logger.Info("Search HospitalSummary Start");
                var client = HttpServiceController.GetService<HospitalSummaryClient>();
                var queryContent = new HospitalSummaryContent("https://newdytapi.ynhdkc.com/index/doctor?hos_code=872018&dep_id=960&from_date=2022-05-02&end_date=2022-05-10&reg_date=2017-2-20&vip=0");
                client.Search(queryContent);
            }
            catch(Exception ex)
            {
                GLog.Logger.Error(ex);
            }
        }

        private void DianYiTong_Click(object sender, RoutedEventArgs e)
        {

        }

        private void shanxi_Click(object sender, RoutedEventArgs e)
        {
            Platform.shanxi.Initializer.Init();
        }

        private void yunnan_Click(object sender, RoutedEventArgs e)
        {
            Platform.yunnan.Initializer.InitAsync();
        }
    }
}
