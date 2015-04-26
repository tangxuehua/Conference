using System;
using System.ServiceProcess;

namespace Payments.ProcessorHost
{
    class Program
    {
        static void Main()
        {
            if (!Environment.UserInteractive)
            {
                ServiceBase.Run(new Service1());
            }
            else
            {
                Bootstrap.Initialize();
                Bootstrap.Start();
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
        }
    }
}
