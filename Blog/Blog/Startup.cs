using Audit.Core;
using Blog.Cryptography;
using Blog.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // add our custom services we need.
            services.AddSingleton<ICryptographicService, CryptographicService>();

            services.AddDbContext<UserContext>(options =>
                    options.UseInMemoryDatabase("UserContext"));

            services.AddDbContext<BlogEntryContext>(options =>
                    options.UseInMemoryDatabase("BlogEntryContext"));

            services.AddDbContext<CommentContext>(options =>
                    options.UseInMemoryDatabase("CommentContext"));

            services.AddDbContext<FeedContext>(options =>
                options.UseInMemoryDatabase("FeedContext"));
        }


        /// <summary>
        /// A6 -
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // A10 -
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddLog4Net();
            app.UseDeveloperExceptionPage();

            // A6 -
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=BlogEntries}/{action=Index}/{id?}");
            });

            // A10 - 
            Audit.Core.Configuration.Setup()
                .UseLog4net();
        }

    }
}
