using System.Linq;
using System.Net;

namespace Serialization
{
    public class BaseController : IBaseController
    {
        public StorageService<Input> Service { get; set; }

        [RequestMapping]
        public HttpResponse Ping()
        {
            var response = new HttpResponse(HttpStatusCode.OK);
            return response;
        }

        [RequestMapping(Method = "POST")]
        public HttpResponse PostInputData(string json)
        {
            var response = new HttpResponse(HttpStatusCode.OK);
            var serializer = new JsonSerializer<Input>();
            var input = serializer.Deserialize(json);

            Service.Save(input);

            return response;
        }

        [RequestMapping]
        public HttpResponse GetAnswer()
        {
            var response = new HttpResponse(HttpStatusCode.OK);

            var input = Service.Data;

            if (input != null)
            {
                var serializer = new JsonSerializer<Output>();

                var output = new Output
                {
                    SumResult = Utils.Sum(input.Sums)*input.K,
                    MulResult = Utils.Multiplication(input.Muls),
                    SortedInputs = Utils.Concat(input.Sums, input.Muls).OrderBy(x => x).ToArray()
                };

                var content = serializer.Serialize(output);
                response.Body = System.Text.Encoding.UTF8.GetBytes(content);
            }
            else
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        [RequestMapping]
        public HttpResponse Stop()
        {
            var response = new HttpResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
