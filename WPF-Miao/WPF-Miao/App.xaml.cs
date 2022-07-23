using HttpProcessor.Container;
using Logging;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Configuration;
using System.Windows;
using WPF_Miao.Properties;

namespace WPF_Miao
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            InitAppDoman();
            GLog.Logger.Info("***************** Application Start *****************");
            base.OnStartup(e);
        }



        private void InitAppDoman()
        {
            var logPath = Settings.Default.LogPath;
            AppDomain.CurrentDomain.SetData("LogPath", logPath);
        }

        #region Prism

        protected override Window CreateShell()
        {
            var mainWindow = Container.Resolve<MainWindow>();
            return MainWindow;
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        #endregion Prism
    }
}
