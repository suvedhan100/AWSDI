using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWSDI
{
    public class HelloService : IHelloService
    {
        private readonly ILogger<HelloService> _logger;
        public HelloService(ILogger<HelloService> logger)
        {
            _logger = logger;
        }

        public string SayHello(string input)
        {
            _logger.LogInformation("hello entry");
            return "Hey Hello "+input;
        }
    }
}
