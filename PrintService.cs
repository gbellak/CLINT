using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLINT
{
    class PrintService : IPrintService
    {
        private readonly ILogger<PrintService> _log;

        public PrintService(ILogger<PrintService> log)
        {
            _log = log;
        }


        public void Run(string message)
        {
            //string Greeting = string.Join("-", _greeting);
            System.Console.WriteLine(message);

            _log.LogInformation($"Message Received {message}");



        }
    }
}
