using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WPF_Miao.Platform.yunnan.viewModel;

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

            try
            {
                var sessionItem = Container.ServiceProvider.GetService<ISessionItem>();
                DataContext = new YunnanViewModel(logPanel, sessionItem!);
            }
            catch(Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }

        }
    }
}
