using System.Net;

namespace Application.Responses
{
    public class Response<T> : BaseResponse
    {
        public T Type { get; private set; }

        private Response(HttpStatusCode status, bool success, string message, T type) : base(success, message, status)
        {
            Type = type;
        }
        public Response(HttpStatusCode status, T type) : this(status, true, string.Empty, type) { }
        public Response(HttpStatusCode status, string message) : this(status, false, message, default(T)) { }
        
    }
}