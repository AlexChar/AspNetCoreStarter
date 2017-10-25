using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreStarter.Data;
using AspNetCoreStarter.Data.Repositories;
using AspNetCoreStarter.Infrastructure;
using AspNetCoreStarter.Infrastructure.Filters;
using AspNetCoreStarter.Infrastructure.Mappings;
using AspNetCoreStarter.Models;
using AspNetCoreStarter.Services;
using AspNetCoreStarter.ViewModels.Users;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCoreStarter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            //services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddAutoMapper()
                .AddSwaggerDocumentation();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidateModelStateFilter());
            });

            // Add Autofac
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            this.Container = builder.Build();

            return new AutofacServiceProvider(this.Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();

                app.UseSwaggerDocumentation();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
