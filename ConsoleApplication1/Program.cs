using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IRBACService irbacservice = ServiceProxyFactory.Create<IRBACService>();
                using (irbacservice as IDisposable)
                {
                    try
                    {
                        var result = irbacservice.Login(string.Empty, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("InnerException");
                        Console.WriteLine("\tType:{0}", ex.InnerException.GetType());
                        Console.WriteLine("\tMessage:{0}", ex.InnerException.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
