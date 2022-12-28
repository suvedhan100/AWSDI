using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSDI
{
    public class Function
    {
        private IHelloService HelloService;
        private readonly ILogger<Function> _logger;
        public Function()
        {
            var startup = new Startup();
            IServiceProvider provider = startup.ConfigureServices();
            _logger = provider.GetRequiredService<ILogger<Function>>();
            HelloService = provider.GetRequiredService<IHelloService>();
        }
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            _logger.LogInformation("entry");
            return HelloService.SayHello(input);
        }
    }
}
