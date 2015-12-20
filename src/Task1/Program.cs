using System;
using System.Net;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = Console.ReadLine();
            var host = "http://127.0.0.1";
            using (var server = new HttpServer())
            {
                server.Port = port;
                server.Host = host;
                server.Listener = new HttpListener();
                var controller = new BaseController {Service = new StorageService<Input>()};
                server.Controller = controller;
                server.Start();
            }
        }
    }
}
