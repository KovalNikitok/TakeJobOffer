namespace TakeJobOffer.Domain.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; }
        public string Trace {  get; }

        public AppException(int statusCode, string exMessage, string stackTrace)
            : base($"[{statusCode}] {exMessage}\n\nTrace:\n{stackTrace}")
        {
            StatusCode = statusCode;
            Trace = stackTrace;
        }
    }
}
