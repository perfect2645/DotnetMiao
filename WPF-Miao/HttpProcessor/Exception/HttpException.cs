namespace HttpProcessor.ExceptionManager
{
    public class HttpException : Exception
    {
        public string ErrCode { get; private set; }

        public HttpException(string message, string errCode = "error") : base(message)
        {
            ErrCode = errCode;
            Logging.GLog.Logger.Error($"message:{message}, errCode:{errCode}");
        }

        public HttpException(Exception ex, string errCode = "error") : base(ex.Message)
        {
            ErrCode = errCode;
            Logging.GLog.Logger.Error($"errCode:{errCode}\r\n, Exception:{ex.Message}, ");
        }
    }
}
