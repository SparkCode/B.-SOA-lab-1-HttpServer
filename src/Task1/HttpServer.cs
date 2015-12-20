using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace Serialization
{
    public class HttpServer : IDisposable
    {
        public string Host { set; get; }
        public string Port { set; get; }
        public HttpListener Listener { set; get; }
        public IBaseController Controller { set; get; }

        private IDictionary<HttpQuery, MethodInfo> _requestMap;

        private HttpListenerContext _context;

        public void Start()
        {
            _requestMap = RequestMappingAttributeProcessor.GetRequestMap(Controller);
            Listener.Prefixes.Add($"{Host}:{Port}/");
            Listener.Start();
            while (Listener.IsListening)
            {
                try
                {
                    _context = Listener.GetContext();
                    Process();
                }
                catch (Exception exception)
                {
                    SendResponse(HttpStatusCode.InternalServerError);
                    Console.Error.WriteLine(exception);
                }
            }
        }
        
        private void Process()
        {
            var httpRequest = _context.Request;
            var resource = httpRequest.Url.AbsolutePath.Substring(1);
            var httpMethod = httpRequest.HttpMethod;
            var httpQuery = new HttpQuery(resource, httpMethod);

            if (!_requestMap.ContainsKey(httpQuery))
            {
                SendResponse(HttpStatusCode.NotFound);
            }
            else
            {
                var method = _requestMap[httpQuery];
                HttpResponse methodResponse;

                if (httpMethod.Equals("POST"))
                {
                    using (var reader = new StreamReader(_context.Request.InputStream))
                    {
                        var content = reader.ReadToEnd();
                        methodResponse = (HttpResponse)method.Invoke(Controller, new object[] { content });
                    }
                }
                else
                {
                    methodResponse = (HttpResponse)method.Invoke(Controller, null);
                }

                SendResponse(methodResponse.StatusCode, methodResponse.Body);
            }

            if (resource.Equals("Stop"))
            {
                Stop();
            }
        }

        private void SendResponse(HttpStatusCode statusCode, byte[] body = null)
        {
            _context.Response.StatusCode = (int)statusCode;
            if (body != null)
            {
                _context.Response.OutputStream.Write(body, 0, body.Length);
            }
            _context.Response.OutputStream.Close(); // Close stream -> Send response
        }

        private void Stop()
        {
            Listener.Stop();
        }

        public void Dispose()
        {
            Listener.Close();
        }
    }
}
