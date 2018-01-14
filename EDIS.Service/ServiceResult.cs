namespace EDIS.Service
{
    public abstract class ServiceResult
    {
        public bool Success { get; protected set; }

        public string Message { get; protected set; }

        public object ResultObject { get; set; }

        protected ServiceResult(bool success)
        {
            Success = success;
        }

        protected ServiceResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class SuccessServiceResult : ServiceResult
    {
        public SuccessServiceResult()
            : base(true)
        {

        }
    }

    public class FalseServiceResult : ServiceResult
    {
        public FalseServiceResult(string message)
            : base(false, message)
        {

        }
    }
}