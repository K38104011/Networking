using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Http.Server
{
    public class Examples
    {
        public void Do()
        {
            Ex2();
        }

        private void Ex2()
        {
            var server = new WebServer("http://localhost:51111/", @"C:\Users\Giang\Desktop");
            try
            {
                server.Start();
                Console.WriteLine("Server running...press Enter to stop");
                WebClient wc = new WebClient();
                Console.WriteLine(wc.DownloadString("http://localhost:51111/MyApp/Request.txt"));
                Console.ReadLine();
            }
            finally
            {
                server.Stop();
            }
        }

        class WebServer
        {
            private HttpListener _listener;
            private string _baseFolder;

            public WebServer(string uriPrefix, string baseFolder)
            {
                _listener = new HttpListener();
                _listener.Prefixes.Add(uriPrefix);
                _baseFolder = baseFolder;
            }

            public async void Start()
            {
                _listener.Start();
                while (true)
                {
                    try
                    {
                        HttpListenerContext context = await _listener.GetContextAsync();
                        Task.Run(() =>ProcessRequestAsync(context));
                    }
                    catch (HttpListenerException)
                    {
                        break;
                    }
                    catch (InvalidOperationException)
                    {
                        break;
                    }
                }
            }

            public void Stop()
            {
                _listener.Stop();
            }

            async void ProcessRequestAsync(HttpListenerContext context)
            {
                try
                {
                    string fileName = Path.GetFileName(context.Request.RawUrl);
                    string path = Path.Combine(_baseFolder, fileName);
                    byte[] msg;
                    if (!File.Exists(path))
                    {
                        Console.WriteLine("Resource not found: " + path);
                        context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                        msg = Encoding.UTF8.GetBytes("Sorry, that page that not exist");
                    }
                    else
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.OK;
                        msg = File.ReadAllBytes(path);
                    }
                    context.Response.ContentLength64 = msg.Length;
                    using (Stream stream = context.Response.OutputStream)
                    {
                        await stream.WriteAsync(msg, 0, msg.Length);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Request error: " + e);
                }
            }
        }

        private void Ex1()
        {
            System.Net.HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:51111/MyApp/");
            listener.Start();

            Task.Run(async () =>
            {
                HttpListenerContext context = await listener.GetContextAsync();

                string msg = "Hello: " + context.Request.RawUrl;
                context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                context.Response.StatusCode = (int) HttpStatusCode.OK;

                using (Stream stream = context.Response.OutputStream)
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(msg);
                }

                listener.Stop();
            });

            WebClient wc = new WebClient();
            Console.WriteLine(wc.DownloadString("http://localhost:51111/MyApp/Request.txt"));
        }
    }
}
