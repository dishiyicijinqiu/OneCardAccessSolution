using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Core
{
    partial class ExceptionPolicyTest
    {
        public void Test()
        {
            //ExceptionPolicy
            //ExceptionManager exManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();

            //IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();

            //LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            //Logger.SetLogWriter(logWriterFactory.Create());

            //ExceptionPolicyFactory exceptionFactory = new ExceptionPolicyFactory(configurationSource);
            //ExceptionManager manager = exceptionFactory.CreateDefault();
            ExceptionPolicyFactory exceptionPolicyFactory = new ExceptionPolicyFactory(EnterpriseLibraryContainer.Current);
            List<ExceptionPolicyImpl> list1 = new List<ExceptionPolicyImpl>();
            for (int i = 0; i < 5; i++)
            {
                ExceptionPolicyImpl execpolicy = exceptionPolicyFactory.Create(string.Format("policy{0}", i));
            }
            ExceptionManagerImpl manager = new ExceptionManagerImpl(list1, new NullDefaultExceptionHandlingInstrumentationProvider());
        }

        //static void ServiceFactory()
        //{
        //    IUnityContainer container = new UnityContainer();

        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EntLib.Config");
        //    var map = new ExeConfigurationFileMap { ExeConfigFilename = path };
        //    var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

        //    try
        //    {
        //        var section = (UnityConfigurationSection)config.GetSection("unity");
        //        section.Configure(container, "DefContainer");

        //        //From Entlib 6 document:Exception Handling Application Block objects can no longer be created 
        //        // automatically from the Unity DI container. 
        //        IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
        //        LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
        //        Logger.SetLogWriter(logWriterFactory.Create());

        //        ExceptionPolicyFactory exceptionPolicyFactory = new ExceptionPolicyFactory(configurationSource);
        //        ExceptionPolicy.SetExceptionManager(exceptionPolicyFactory.CreateManager());
        //    }
        //    catch (InvalidOperationException ioe)
        //    {
        //        throw;
        //    }

        //}
    }
    public class PolicyInjectionStrategy : BuilderStrategy
    {
        protected static IConfigurationSource GetConfigurationSource(IBuilderContext context)
        {
            IConfigurationObjectPolicy policy
                = context.Policies.Get<IConfigurationObjectPolicy>(typeof(IConfigurationSource));

            if (policy == null)
                return new SystemConfigurationSource();
            else
                return policy.ConfigurationSource;
        }
        public override void PreBuildUp(IBuilderContext context)
        {
            base.PreBuildUp(context);
            if (context.Policies.Get<IBuilderPolicy>(context.BuildKey) == null)
            {
                context.Policies.Set<IBuilderPolicy>(new PolicyInjectionPolicy(true), context.BuildKey);
            }
        }

        public override void PostBuildUp(IBuilderContext context)
        {
            base.PostBuildUp(context);
            IPolicyInjectionPolicy policy = context.Policies.Get<IPolicyInjectionPolicy>(context.BuildKey);
            if ((policy != null) && policy.ApplyPolicies)
            {                
                policy.SetPolicyConfigurationSource(GetConfigurationSource(context));
                context.Existing = policy.ApplyProxy(context.Existing, context.BuildKey.Type);
                //new SystemConfigurationSource();
            }
        }
    }
    public interface IPolicyInjectionPolicy : IBuilderPolicy
    {
        bool ApplyPolicies { get; }
        void SetPolicyConfigurationSource(IConfigurationSource configSource);
        object ApplyProxy(object instanceToProxy, Type typeToProxy);
    }

    public class PolicyInjectionPolicy : IPolicyInjectionPolicy
    {
        private readonly bool applyPolicies;
        private volatile IConfigurationSource configSource;
        private PolicyInjector injector;
        private object policiesLock = new object();

        public PolicyInjectionPolicy(bool applyPolicies)
        {
            this.applyPolicies = applyPolicies;
        }
        public bool ApplyPolicies
        {
            get { return applyPolicies; }
        }

        #region IPolicyInjectionPolicy Members
        public void SetPolicyConfigurationSource(IConfigurationSource configSource)
        {
            if (this.configSource == null || this.configSource != configSource)
            {
                lock (policiesLock)
                {
                    if (this.configSource == null || this.configSource != configSource)
                    {
                        this.configSource = configSource;
                        //PolicyInjectorFactory injectorFactory =
                        //    new PolicyInjectorFactory(configSource);
                        //injector = injectorFactory.Create();
                        injector = new PolicyInjector(this.configSource);
                    }
                }
            }
        }

        #endregion
        public object ApplyProxy(object instanceToProxy, Type typeToProxy)
        {
            return injector.Wrap(typeToProxy, instanceToProxy);
        }
    }
    public interface IConfigurationObjectPolicy : IBuilderPolicy
    {
        /// <summary>
        /// Gets the configuration source.
        /// </summary>
        IConfigurationSource ConfigurationSource { get; }
    }

}
