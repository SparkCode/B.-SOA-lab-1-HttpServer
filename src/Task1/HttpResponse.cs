using System.Net;

namespace Serialization
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { set; get; }
        public byte[] Body { set; get; }

        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
