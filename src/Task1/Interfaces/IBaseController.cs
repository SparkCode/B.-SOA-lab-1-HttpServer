using System;

namespace Serialization
{
    public interface IBaseController
    {
        HttpResponse Ping();
        HttpResponse PostInputData(String json);
        HttpResponse GetAnswer();
        HttpResponse Stop();
    }
}
