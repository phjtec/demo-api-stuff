using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace DemoUserSaveAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            {

            var cloudMode = (Environment.GetEnvironmentVariable("PORT") ?? "false") != "false";
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            var url = String.Concat("http://0.0.0.0:", port);

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var host = webBuilder.UseStartup<Startup>();//.UseUrls;
                    if(cloudMode)
                    {
                        host.UseUrls(url);
                    }
        });
        }
    }
}
