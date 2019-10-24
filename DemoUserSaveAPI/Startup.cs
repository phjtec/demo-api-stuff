using DemoUserSaveAPI.Configuration;
using DemoUserSaveAPI.GCloud.Data.IO.Providers;
using DemoUserSaveAPILibs.Core.Configuration;
using DemoUserSaveAPILibs.Core.Data.Entity;
using DemoUserSaveAPILibs.Core.Data.IO.Providers;
using DemoUserSaveAPILibs.Core.Tasks;
using DemoUserSaveAPILibs.Domain.UserProfile.Repositories;
using DemoUserSaveAPILibs.Domain.UserProfile.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace DemoUserSaveAPI
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
            services.AddControllers();

            var host = Configuration.GetValue<string>("EMULATOR_HOST");
            var project = Configuration.GetValue<string>("PROJECT_ID");

            var configData = new Dictionary<string, object>();
            foreach (var config in Configuration.AsEnumerable())
            {
                configData.Add(config.Key, config.Value);
            }

            services.AddTransient<IUserProfileService, UserProfileService>();// ((s) => new UserProfileService(host, project));
            services.AddTransient<ITaskThreadManagementService, TaskThreadManagementService>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IDataEntityObjectFactory, DataEntityObjectFactory>();
            services.AddTransient<IDataAccessProvider, DatastoreDbProvider>();
            services.AddTransient<IConfigurationService, ApiConfigurationService>((s) => new ApiConfigurationService(configData));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });        
        }
    }
}
