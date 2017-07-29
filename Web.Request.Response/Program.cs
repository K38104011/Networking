using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Web.Request.Response
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

        static void Ex1()
        {
            Task.Run(async () =>
            {
                string uri = "https://github.com/K38104011/DataStructureLearning";
                System.Net.WebRequest req = System.Net.WebRequest.Create(uri);
                using (System.Net.WebResponse res = await req.GetResponseAsync())
                using (System.IO.Stream stream = res.GetResponseStream())
                using (System.IO.FileStream fs = System.IO.File.Create("code.html"))
                {
                    stream?.CopyTo(fs);
                    Console.WriteLine(res.ContentLength);
                }
                System.Diagnostics.Process.Start("code.html");
            });
        }
    }
}
