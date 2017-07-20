using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace WebClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Example5();
            Console.ReadKey();
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
