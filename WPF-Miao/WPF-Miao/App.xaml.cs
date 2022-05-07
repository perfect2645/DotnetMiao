using HttpProcessor.Container;
using Logging;
using System;
using System.Configuration;
using System.Windows;
using WPF_Miao.Properties;

namespace WPF_Miao
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitAppDoman();
            InitHttpController();
        }

        private void InitAppDoman()
        {
            var logPath = Settings.Default.LogPath;
            AppDomain.CurrentDomain.SetData("LogPath", logPath);
        }

        private void InitHttpController()
        {
            var controller = HttpServiceController.Instance;
            GLog.Logger.Info("InitHttpController");
        }
    }
}
