using HttpProcessor.Container;
using Logging;
using Microsoft.Extensions.DependencyInjection;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DianYiTong_Click(object sender, RoutedEventArgs e)
        {
            new DianYiTongMain();
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            //GLog.GetLogger().Info("SubmitOrder---start");
            //var submitOrder = new SubmitOrder();
            //submitOrder.TrySubmitOrder();
        }

        private void Hospital_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //HttpServiceController./*Instance*/
                GLog.Logger.Info("Hospital_Click");
                var queryHospitalSummaryClient = new HttpClient();
                var service = new QueryHospitalSummaryService(queryHospitalSummaryClient);

                //[TODO url serialize]
                var queryContent = new HospitalSummaryContent("https://newdytapi.ynhdkc.com/index/doctor?hos_code=872018&dep_id=960&from_date=2022-05-02&end_date=2022-05-10&reg_date=2017-2-20&vip=0");
                service.Search(queryContent);
            }
            catch(Exception ex)
            {
                GLog.Logger.Error(ex);
            }

        }
    }
}
