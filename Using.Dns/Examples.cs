using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Using.Dns
{
    public class Examples
    {
        public void Do()
        {
            Ex1();
        }

        private void Ex1()
        {
            foreach (IPAddress ipAddress in System.Net.Dns.GetHostAddresses("google.com.vn"))
            {
                Console.WriteLine(ipAddress);
                Console.WriteLine(System.Net.Dns.GetHostEntry(ipAddress).HostName);
            }
        }
    }
}
