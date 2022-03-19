using System.Net;

namespace Application.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }

        public BaseResponse(bool success, string message, HttpStatusCode status)
        {
            Success = success;
            Message = message;
            Status = status;
        }
        
    }
}