using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace AspNetCoreStarter.Infrastructure.Modules
{
    public class StoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Store"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
                //.SingleInstance();
        }
    }
}
