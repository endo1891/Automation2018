using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving;
using Autofac.Core.Registration;

namespace EvernoteCommon.Shared.Logging
{
    public class LoggingModule: Module
    {
        /*protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            {
                registration.Preparing += ComponentRegistration.OnPreparing;
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) => LogProvider.GetLogger(p.TypedAs<Type>()))
            .As<ILog>()
                .InstancePerDependency();
        }

       private static void ComponentRegistrationOnPreparing(Object sender, PreparingEventArgs args)
        {
            var forType = args.Component.Activator.LimitType;
            var loggerParameter = new ResolvedParameter(
                (p, c) => p.ParameterType == typeof(ILog),
                (p, c) =>

                {
                    var instanceLookup = c as IInstanceLookup;
                    Type properType = null;

                    if (instanceLookup != null)
                        properType = instanceLookup.ComponentRegistration.Activator.LimitType;

                    return c.Resolve<ILog>(TypedParameter.From(properType ?? forType));
                });
            args.Parameters = args.Parameters.Union(new[] { loggerParameter });
                }*/

        }
    }

