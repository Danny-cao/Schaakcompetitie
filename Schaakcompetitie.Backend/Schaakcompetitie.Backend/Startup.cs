using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schaakcompetitie.Backend.DAL;
using Schaakcompetitie.Backend.DAL.DataMapper;
using Schaakcompetitie.Backend.DAL.Models;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


namespace Schaakcompetitie.Backend
{
    [ExcludeFromCodeCoverage]
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
            services.AddControllers();
            services.AddTransient<IDataMapper<Partij, long>, PartijDataMapper>();
            services.AddTransient<IDataMapper<Speler, long>, SpelerDataMapper>();
            ConfigureEntityFramework(services);
            services.UseRoger(Configuration);
        }
        
        private void ConfigureEntityFramework(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("MySql");
            const int maxRetryCount = 5;
            const int secondsToWait = 5;

            services.AddDbContextPool<SchaakContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, mysqlOptions =>
                    {
                        mysqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend);
                        mysqlOptions.EnableRetryOnFailure(maxRetryCount, TimeSpan.FromSeconds(secondsToWait), null);
                    })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SchaakContext context)
        {
            context.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}