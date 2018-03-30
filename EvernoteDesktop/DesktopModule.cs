using Autofac;
using EvernoteCommon;

namespace EvernoteDesktop
{
    public class DesktopModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<TestingModule>();
            builder.RegisterAssemblyPageObjectModels(ThisAssembly);
        }
    }
}