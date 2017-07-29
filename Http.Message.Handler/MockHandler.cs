using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Http.Message.Handler
{
    public class MockHandler : System.Net.Http.HttpMessageHandler
    {
        private Func<System.Net.Http.HttpRequestMessage, System.Net.Http.HttpResponseMessage> _responseGenerator;

        public MockHandler(Func<HttpRequestMessage, HttpResponseMessage> responseGenerator)
        {
            _responseGenerator = responseGenerator;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = _responseGenerator(request);
            response.RequestMessage = request;
            return Task.FromResult(response);
        }
    }
}
