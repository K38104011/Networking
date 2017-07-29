using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cookies
{
    public class Examples
    {
        public void Do()
        {
            Ex2();
        }

        private void Ex2()
        {
            System.Net.CookieContainer cookieContainer = new CookieContainer();
            System.Net.Http.HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookieContainer;
            System.Net.Http.HttpClient httpClient = new HttpClient(handler);
            System.Threading.Tasks.Task.Run(async () =>
            {
                System.Net.Http.HttpResponseMessage response = await httpClient.GetAsync("http://www.google.com");
                var cookies = response.Headers.GetValues("Set-Cookie");
                foreach (var cookie in cookies)
                {
                    Console.WriteLine(cookie);
                }
            });
        }

        private void Ex1()
        {
            System.Net.CookieContainer cookieContainer = new CookieContainer();
            System.Net.HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://www.google.com");
            request.Proxy = null;
            request.CookieContainer = cookieContainer;
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                foreach (Cookie cookie in response.Cookies)
                {
                    Console.WriteLine("Name:" + cookie.Name);
                    Console.WriteLine("Value:" + cookie.Value);
                    Console.WriteLine("Path:" + cookie.Path);
                    Console.WriteLine("Domain" + cookie.Domain);
                }
            }
        }
    }
}
