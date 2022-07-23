using System.Threading.Tasks;

namespace Dian
{
    internal static class Initializer
    {
        public static async Task InitAsync()
        {
            await Task.Factory.StartNew(() => InitHttpContainer());
            await Task.Factory.StartNew(() => InitViewContainer());
        }
        private static void InitHttpContainer()
        {
/*            //var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            HttpServiceController.AddTransientService<UserInfoController, AppointmentHandler>();
            HttpServiceController.ServiceCollection.AddTransient<UserInfoContent>();

            HttpServiceController.AddTransientService<AppointmentController, AppointmentHandler>();
            HttpServiceController.AddTransientService<GetTimestampController>();
            HttpServiceController.ServiceCollection.AddTransient<AppointmentContent>();
            HttpServiceController.ServiceCollection.AddTransient<SecureHeader>();
            HttpServiceController.ServiceCollection.BuildServiceProvider();
            HttpServiceController.BuidServiceProvider();*/
        }

        private static void InitViewContainer()
        {
/*            Container.ServiceCollection.AddTransient<ISessionItem, SessionItem>();
            Container.BuildServiceProvider();*/
        }
    }
}
