using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Http.Message.Handler
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mocker = new MockHandler(request => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("You asked for " + request.RequestUri)
            });
            System.Net.Http.HttpClient client = new HttpClient(mocker);
            Task.Run(async () =>
            {
                var response = await client.GetAsync("http://www.linqpad.net");
                string result = await response.Content.ReadAsStringAsync();
                Assert.AreEqual("You asked for http://www.linqpad.net", result);
            });
        }
    }
}
