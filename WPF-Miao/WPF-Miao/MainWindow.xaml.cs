using Logging;
using System.Windows;
using WPF_Miao.Platform;

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
            GLog.GetLogger().Info("SubmitOrder---start");
            var submitOrder = new SubmitOrder();
            submitOrder.TrySubmitOrder();
        }
    }
}
