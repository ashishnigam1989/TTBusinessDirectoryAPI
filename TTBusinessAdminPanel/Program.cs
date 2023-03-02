using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTBusinessAdminPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
        //    var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
           CreateHostBuilder(args).Build().Run();
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).ConfigureLogging((hcontext,logging) => {
                logging.AddConfiguration(hcontext.Configuration.GetSection("Logging"));
                logging.AddConsole();
              //  logging.AddDebug();
              //  logging.AddEventSourceLogger();
               // logging.AddNLogWeb();
            })
            ;

    }
}
