using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Using.Tcp
{
    public class Examples
    {
        public void Do()
        {
            new Thread(Server).Start();
            Thread.Sleep(500);
            Client();
        }

        private void Client()
        {
            using (System.Net.Sockets.TcpClient client = new TcpClient("localhost", 51111))
            {
                using (NetworkStream n = client.GetStream())
                {
                    BinaryWriter w = new BinaryWriter(n);
                    w.Write("Hello");
                    w.Flush();
                    Console.WriteLine(new BinaryReader(n).ReadString());
                }
            }
        }

        private void Server()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 51111);
            listener.Start();
            using (TcpClient client = listener.AcceptTcpClient())
            {
                using (NetworkStream networkStream = client.GetStream())
                {
                    string msg = new BinaryReader(networkStream).ReadString();
                    Console.WriteLine(msg);
                    BinaryWriter w = new BinaryWriter(networkStream);
                    w.Write(msg + " right back!");
                    w.Flush();
                }
            }
            listener.Stop();
        }
    }
}
