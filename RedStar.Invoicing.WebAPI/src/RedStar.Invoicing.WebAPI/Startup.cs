using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedStar.Invoicing.Commands;
using RedStar.Invoicing.DocumentDb;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddOptions();

            services.Configure<DocumentDbSettings>(Configuration.GetSection("DocumentDbSettings"));

#if DEBUG
            services.AddTransient<IGetUserSettingsQuery, FileSystem.Queries.GetUserSettingsQuery>();
            services.AddTransient<IPersistUserSettingsCommand, FileSystem.Commands.PersistUserSettingsCommand>();
#else
            services.AddTransient<IGetUserSettingsQuery, DocumentDb.Queries.GetUserSettingsQuery>();
            services.AddTransient<IPersistUserSettingsCommand, DocumentDb.Commands.PersistUserSettingsCommand>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
