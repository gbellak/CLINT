using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CLINT
{
    class GreetingService : IGreetingService
    {
        private readonly ILogger<GreetingService> _log;
        private readonly IConfiguration _config;
        private readonly IPrintService _printsvc;
        private readonly string _greeting;
        private readonly string _greeting2;

        public GreetingService(ILogger<GreetingService> log, IConfiguration config, IPrintService printsvc, string greeting, string greeting2)
        {
            _log = log;
            _config = config;
            _printsvc = printsvc;
            _greeting = greeting;
            _greeting2 = greeting2;
        }
        public void Run()
        {
            //string Greeting = string.Join("-", _greeting);
            System.Console.WriteLine(_greeting);
            System.Console.WriteLine(_greeting2);
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                _log.LogInformation("Run number {runNumber}", i);

            }

            _printsvc.Run(_greeting+" " +_greeting2);
        }
    }
}
