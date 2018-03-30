using Autofac;
using EvernoteCommon;
using SpecFlow.Autofac;
using System;
using System.Reflection;
using EvernoteDesktop;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Linq;
using TechTalk.SpecFlow;

namespace EvernoteDesktop
{

    public static class AutofacContainerExtensions
    {
        
        public static void RegisterAssemblyPageObjectModels(this ContainerBuilder builder, Assembly whichAssembly)
        {
            builder.RegisterAssemblyTypes(whichAssembly)
                .Where(x => x.Name.EndsWith("Page") && !x.IsAbstract && !x.IsInterface)
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        [ScenarioDependencies]
        public static ContainerBuilder CreateContainerBuilder()
        {
            // create container with the runtime dependencies
            var builder = new ContainerBuilder();
            //TODO: add customizations, stubs required for testing

            //builder.RegisterTypes(typeof(TestingModule).Assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))).ToArray()).InstancePerLifetimeScope();


            builder.Register(c =>
                {
                    IComponentContext ctx = c.Resolve<IComponentContext>();
                    TestingModule modules = ctx.Resolve<TestingModule>();
                    return modules;
            });

            return builder;
        }

        
    }
}