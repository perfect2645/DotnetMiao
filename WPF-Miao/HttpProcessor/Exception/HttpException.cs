namespace HttpProcessor.ExceptionManager
{
    public class HttpException : Exception
    {
        public int StatusCode { get; private set; }

        public HttpException(string message, int statusCode = 200): base(message)
        {
            StatusCode = statusCode;
        }
    }
}
