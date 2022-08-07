namespace HttpProcessor.ExceptionManager
{
    public class HttpException : Exception
    {
        public string ErrCode { get; private set; }

        public HttpException(string message, string errCode = "error") : base(message)
        {
            ErrCode = errCode;
        }

        public HttpException(Exception ex, string errCode = "error") : base(ex.StackTrace)
        {
            ErrCode = errCode;
        }
    }
}
