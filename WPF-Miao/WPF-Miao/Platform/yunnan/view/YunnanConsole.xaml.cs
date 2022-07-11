using System.Windows;

namespace WPF_Miao.Platform.yunnan.view
{
    /// <summary>
    /// YunnanConsole.xaml 的交互逻辑
    /// </summary>
    public partial class YunnanConsole : Window
    {
        public YunnanConsole()
        {
            InitializeComponent();
            DataContext = new YunnanViewModel();
        }
    }
}
