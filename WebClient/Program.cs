using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Example11();
            Console.ReadKey();
        }

        static async void Example11()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.DownloadProgressChanged += (sender, args) =>
            {
                Console.WriteLine(args.ProgressPercentage + "% complete");
            };
            await Task.Delay(5000).ContinueWith(ant => wc.CancelAsync());
            try
            {
                await wc.DownloadFileTaskAsync("https://www.oreilly.com/", "code.html");
            }
            catch (WebException we)
            {
                Console.WriteLine(we.Status == WebExceptionStatus.RequestCanceled);
            }
        }

        static void Example10()
        {
            string address = @"C:\Users\Giang\Desktop\UploadInHear.txt";
            string postData = "GiangPDH";
            byte[] postArray = System.Text.Encoding.ASCII.GetBytes(postData);
            System.Net.WebClient wc = new System.Net.WebClient();
            Stream postStream = wc.OpenWrite(address);
            postStream.Write(postArray, 0, postArray.Length);
            postStream.Close();
        }

        static void Example9()
        {
            string address = @"C:\Users\Giang\Desktop\UploadInHear.txt";
            System.Net.WebClient wc = new System.Net.WebClient();
            NameValueCollection nameValueCollection = new NameValueCollection()
            {
                { "Name", "Giang" },
                { "Age", "23" }
            };
            byte[] response = wc.UploadValues(address, nameValueCollection);
            Console.WriteLine(response.Length);
        }

        static void Example8()
        {
            string postData = "ThaoPTP";
            string address = @"C:\Users\Giang\Desktop\UploadInHear.txt";
            byte[] postArray = System.Text.Encoding.ASCII.GetBytes(postData);
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] response = wc.UploadData(address, postArray);
            Console.WriteLine(System.Text.Encoding.ASCII.GetString(response));
        }

        static void Example7()
        {
            string data = "GiangPDH";
            string address = @"C:\Users\Giang\Desktop\UploadInHear.txt";
            System.Net.WebClient wc = new System.Net.WebClient();
            string response = wc.UploadString(address, data);
            Console.WriteLine(response);
        }

        static void Example6()
        {
            string address = @"C:\Users\Giang\Desktop\e6.png";
            string fileName = @"c:\Users\Giang\Documents\Visual Studio 2015\Projects\Networking\WebClient\bin\Debug\e3.png";
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] response = wc.UploadFile(address, fileName);
        }

        static void Example5()
        {
            System.Net.HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://mp3.zing.vn/");
            System.Net.HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.ContentEncoding);
            System.IO.Stream stream = response.GetResponseStream();
            System.IO.Stream decompressStream = new GZipStream(stream, CompressionMode.Decompress);
            string s = string.Empty;
            using (System.IO.StreamReader reader = new StreamReader(decompressStream, Encoding.UTF8))
            {
                s = reader.ReadToEnd();
            }
            Console.WriteLine(s);
        }

        static void Example4()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Proxy = null;
            System.IO.Stream stream =
                wc.OpenRead("http://zmp3-photo-td.zadn.vn/thumb/240_240/covers/9/a/9a156337c55bdc47e1f65963c24077df_1499654363.jpg");
            using (System.Drawing.Image image = Image.FromStream(stream))
            {
                image.Save("e4.png");
            }
            System.Diagnostics.Process.Start("e4.png");
        }

        static void Example3()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Proxy = null;
            byte[] data =
                wc.DownloadData(
                    "http://zmp3-photo-td.zadn.vn/banner/8/1/8189867b2ee986b1893a02d900ef449c_1500461422.png");
            using (System.Drawing.Image image = Image.FromStream(new MemoryStream(data)))
            {
                image.Save("e3.png", ImageFormat.Png);
            }
            System.Diagnostics.Process.Start("e3.png");
        }

        static void Example2()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Proxy = null;
            string s = wc.DownloadString("http://www.nhaccuatui.com/");
            Console.WriteLine(s);
        }

        static void Example1()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Proxy = null;
            wc.DownloadFile("http://static.mp3.zdn.vn/skins/zmp3-v4.2/images/logo.png", "logo.png");
            System.Diagnostics.Process.Start("logo.png");
        }
    }
}
