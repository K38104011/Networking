using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Uploading.Form.Data
{
    public class Examples
    {
        public void Do()
        {
            Ex3();
        }

        private void Ex3()
        {
            string uri = "http://www.albahari.com/EchoPost.aspx";
            var client = new HttpClient();
            var dict = new System.Collections.Generic.Dictionary<string, string>()
            {
                { "Name", "Giang" },
                { "Company", "Ex3" }
            };
            System.Net.Http.FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(dict);
            System.Threading.Tasks.Task.Run(async () =>
            {
                System.Net.Http.HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            });
        }

        private void Ex2()
        {
            System.Net.WebRequest webRequest = WebRequest.Create("http://www.albahari.com/EchoPost.aspx");
            webRequest.Proxy = null;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            string reqString = "Name=Giang&Company=Ex2";
            byte[] reqData = System.Text.Encoding.UTF8.GetBytes(reqString);
            webRequest.ContentLength = reqData.Length;
            using (Stream reqStream = webRequest.GetRequestStream())
            {
                reqStream.Write(reqData, 0, reqData.Length);
            }
            using (System.Net.WebResponse response = webRequest.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
        }

        private void Ex1()
        {
            System.Net.WebClient wc = new WebClient();
            wc.Proxy = null;
            var data = new System.Collections.Specialized.NameValueCollection();
            data.Add("Name", "Giang");
            data.Add("Company", "Fsoft");
            byte[] result = wc.UploadValues("http://www.albahari.com/EchoPost.aspx", "POST", data);
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(result));
        }
    }
}
