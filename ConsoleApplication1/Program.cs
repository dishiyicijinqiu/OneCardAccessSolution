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
        static void Main(string[] args)
        {
            Test2();
            Console.ReadKey();
        }
        static void Test2()
        {
            var Container = new UnityContainer();
            Container.RegisterInstance<IConnectService>(new ConnectService())
      .AddNewExtension<Interception>().Configure<Interception>()
      .SetDefaultInterceptorFor<IConnectService>(new TransparentProxyInterceptor())
      .AddPolicy("loadingpolicy")
      .AddMatchingRule(new TypeMatchingRule(typeof(IConnectService)))
      .AddCallHandler(typeof(LoadingCallHandler));
            var connectService = Container.Resolve<IConnectService>();
            connectService.Login(string.Empty, string.Empty);
        }
        static void Test1()
        {
            var Container = new UnityContainer();
            Container.RegisterType<IConnectService, ConnectService>()
      .AddNewExtension<Interception>().Configure<Interception>()
      .SetDefaultInterceptorFor<IConnectService>(new TransparentProxyInterceptor())
      .AddPolicy("loadingpolicy")
      .AddMatchingRule(new TypeMatchingRule(typeof(IConnectService)))
      .AddCallHandler(typeof(LoadingCallHandler));
            var connectService = Container.Resolve<IConnectService>();
            connectService.Login(string.Empty, string.Empty);
        }
    }
    public interface IConnectService
    {
        bool Login(string username, string password);
    }
    public class ConnectService : IConnectService
    {
        public bool Login(string username, string password)
        {
            Thread.Sleep(3000);
            return true;
        }
    }


    public class LoadingCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("start");
            IMethodReturn result = getNext()(input, getNext);
            Console.WriteLine("end");
            return result;
        }
        public int Order { get; set; }
    }
}
