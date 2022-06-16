namespace WPF_Miao.Platform.yunnan.session
{
    internal class AppSession
    {
        private static int _localTimeOffset = 0;
        public static int LocalTimeOffset
        {
            get
            {
                return ++_localTimeOffset;
            }
        }
    }
}
