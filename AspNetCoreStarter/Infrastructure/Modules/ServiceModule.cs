using System.Reflection;
using AspNetCoreStarter.Data.Repositories.Users;
using AspNetCoreStarter.Models;
using Autofac;
using Microsoft.AspNetCore.Identity;
using Module = Autofac.Module;

namespace AspNetCoreStarter.Infrastructure.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterType<PasswordHasher<ApplicationUser>>().As<IPasswordHasher<ApplicationUser>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
