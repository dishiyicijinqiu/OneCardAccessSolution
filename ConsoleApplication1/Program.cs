using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static IEventAggregator eventAggregator;
        static void Main(string[] args)
        {
            eventAggregator = new EventAggregator();
            eventAggregator.GetEvent<BaseSenderEvent<string, string>>().
                Subscribe(OnEventLL, ThreadOption.PublisherThread, true, null);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                    break;
                string[] inputs = input.Split(new char[] { ',' });
                eventAggregator.GetEvent<BaseSenderEvent<string, string>>().
                     Publish(inputs[0], inputs[1]);
            }
            Console.ReadKey();
        }
        static void OnEventLL(string sender, string args)
        {
            Console.WriteLine("sender:{0}", sender);
            Console.WriteLine("args:{0}", args);
        }
    }
}
