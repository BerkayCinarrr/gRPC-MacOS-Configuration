using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Runtime.InteropServices;

namespace GrpcGreeter
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
                    // Configuration for macOS
                    // HTTP/2 without TLS should only be used during app development. Production apps should always use transport security. Visit https://go.microsoft.com/fwlink/?linkid=2099682
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) 
                     webBuilder.ConfigureKestrel(options =>
                     {
                         // Setup a HTTP/2 endpoint without TLS.
                         options.ListenLocalhost(5000, o => o.Protocols = 
                             HttpProtocols.Http2);
                     });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
