using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GammaForums
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureAppConfiguration((builderContext, config) =>
                    {
                        IHostEnvironment env = builderContext.HostingEnvironment;
                        config.AddJsonFile(
                            "storageSettings.json",
                            optional: false,
                            reloadOnChange: true);
                    })
                    .UseStartup<Startup>();
                });
    }
}
