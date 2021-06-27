using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Schaakcompetitie.Frontend.DAL;
using Schaakcompetitie.Frontend.DAL.DataMappers;
using Schaakcompetitie.Frontend.DAL.Models;

namespace Schaakcompetitie.Frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IDataMapper<Stand, long>, StandDataMapper>();
            services.AddTransient<IDataMapper<Partij, long>, PartijDataMapper>();
            
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddSingleton(Configuration);
            ConfigureEntityFramework(services);
            services.UseRoger(Configuration);
        }
        
        private void ConfigureEntityFramework(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("MySql");
            const int maxRetryCount = 5;
            const int secondsToWait = 5;

            services.AddDbContextPool<FrontendContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, mysqlOptions =>
                    {
                        mysqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend);
                        mysqlOptions.EnableRetryOnFailure(maxRetryCount, TimeSpan.FromSeconds(secondsToWait), null);
                    })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FrontendContext context)
        {
            context.Database.EnsureCreated();
            
            var proxy_prefix = Environment.GetEnvironmentVariable("PROXY_PREFIX");

            if (proxy_prefix != null)
            {
                app.Use((context, next) =>
                {
                    context.Request.PathBase = new PathString(proxy_prefix);
                    return next();
                });
                app.UseForwardedHeaders();
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}