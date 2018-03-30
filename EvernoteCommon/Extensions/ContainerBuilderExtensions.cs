using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Reflection;
using Autofac;
using AutoMapper;
using AutoMapper.Mappers;
using System.Collections.Specialized;

namespace EvernoteCommon.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterAutoMapper(this ContainerBuilder builder)
        {
                builder.Register(ctx => new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers))
                    .AsImplementedInterfaces()
                    .SingleInstance()
                    .OnActivating(
                    x =>

                    {
                    foreach (var profile in x.Context.Resolve<IEnumerable<Profile>>().Reverse())
                     {
                    x.Instance.AddProfile(profile);
                }
            }).OnActivated(y => y.Instance.Seal());
        

         builder.RegisterType<MappingEngine>()
             .As<IMappingEngine>()
             .SingleInstance();
         }

        public static void RegisterConfiguration(this ContainerBuilder builder, Assembly assembly)
        {
            var configSections = assembly.GetTypes()
                .Where(t => t.Name.EndsWith("ConfigurationSection"))
                .Where(t => !t.IsInterface);

            IList<string> loadedConfigs = new List<string>();

            foreach (var configSection in configSections)
            {
                var configuration = ConfigurationManager.GetSection(configSection.Name);

                if (configuration == null)
                    continue;

                var interfaceType = configSection.GetInterfaces()
                    .First(t => t.Name.EndsWith(configSection.Name));

                builder.RegisterInstance(configuration)
                    .As(interfaceType)
                    .SingleInstance();

                //Add name of config section without suffix section
                loadedConfigs.Add(configSection.Name.Remove(configSection.Name.Length - 7));

                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => loadedConfigs.Contains(t.Name))
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }

        public static void RegisterSingletonsUsingSuffix(this ContainerBuilder builder, Assembly assembly, string suffix)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith(suffix))
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        public static void RegisterDependencyUsingSuffix(this ContainerBuilder builder, Assembly assembly, string suffix)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith(suffix))
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        public static void RegisterSingletonMappingProfiles(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("MappingProfile"))
                .As<Profile>()
                .SingleInstance();
        }
    }
}
