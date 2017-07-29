using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Ex7();
            Console.Read();
        }

        static void Ex7()
        {
            string search = Uri.EscapeDataString("(WebClient OR HttpClient)");
            string language = Uri.EscapeDataString("fr");
            string requestUri = "http://www.google.com/search?q=" + search + "&hl=" + language;
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Threading.Tasks.Task.Run(async () =>
            {
                using (System.IO.FileStream fs = File.Create("Ex7.html"))
                {
                    var response = await client.GetAsync(requestUri);
                    await response.Content.CopyToAsync(fs);
                }
                System.Diagnostics.Process.Start("Ex7.html");
            });
        }

        static void Ex6()
        {
            System.Net.WebClient wc = new WebClient();
            wc.Proxy = null;
            wc.QueryString.Add("q", "WebClient");
            wc.QueryString.Add("hl", "fr");
            wc.DownloadFile("http://www.google.com/search", "Ex6.html");
            System.Diagnostics.Process.Start("Ex6.html");
        }

        static void Ex5()
        {
            System.Net.WebClient wc = new WebClient();
            wc.Proxy = null;
            wc.Headers.Add("CustomHeader", "JustPlaying/1.0");
            wc.DownloadString("http://www.oreilly.com");
            foreach (string name in wc.ResponseHeaders.Keys)
            {
                Console.WriteLine(name + "=" + wc.ResponseHeaders[name]);
            }
        }

        static void Ex4()
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(new HttpClientHandler() { UseProxy = false });
                System.Net.Http.HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://www.albahari.com/EchoPost.aspx");
                request.Content = new StringContent("Ex4");
                System.Net.Http.HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            });
            
        }

        static void Ex3()
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Get, "http://www.linqpad.net");
                System.Net.Http.HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                using (System.IO.FileStream fs = System.IO.File.Create("Ex3.html"))
                {
                    await response.Content.CopyToAsync(fs);
                }
                System.Diagnostics.Process.Start("Ex3.html");
            });
        }

        static void Ex2()
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage response = await client.GetAsync("http://www.linqpad.net");
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                using (System.IO.FileStream fs = System.IO.File.Create("Ex2.txt"))
                {
                    await response.Content.CopyToAsync(fs);
                }
                System.Diagnostics.Process.Start("Ex2.txt");
            });
        }

        static void Ex1()
        {
            System.Net.Http.HttpClientHandler handler = new HttpClientHandler();
            handler.UseProxy = false;
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient(handler);
            System.Threading.Tasks.Task.Run(async () =>
            {
                Console.WriteLine(await httpClient.GetStringAsync("http://www.linqpad.net"));
                Console.WriteLine(await httpClient.GetStringAsync("http://www.albahari.com"));
            });
        }
    }
}
