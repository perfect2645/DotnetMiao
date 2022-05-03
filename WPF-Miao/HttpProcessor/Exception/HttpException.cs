namespace HttpProcessor.ExceptionManager
{
    public class HttpException : HttpRequestException
    {
        public string ErrCode { get; private set; }

        public HttpException(string message, string errCode = "OK"): base(message)
        {
            ErrCode = errCode;
        }
    }
}
