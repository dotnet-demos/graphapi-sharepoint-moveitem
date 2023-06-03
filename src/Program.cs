using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        async static Task Main(string[] args) =>
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddHostedService<MenuService>();
                    services.AddSingleton<MoveFolderAcrosDriveOption>();
                    services.AddSingleton<TestConnectivityOption>();
                    services.AddSingleton((provider) =>
                    {
                        var config = provider.GetRequiredService<IConfiguration>();
                        TokenCredential tokenprovider = new UsernamePasswordCredential(
                            config["Azure:UserName"],
                            config["Azure:Password"],
                            config["Azure:TenantId"],
                            config["Azure:ClientId"]);
                        var result = new GraphServiceClient(tokenprovider);
                        return result;
                    });
                })

                //.UseConsoleLifetime() // This may be used when running inside container. But we dont really run an interative menu program in container.
                .Build()
                .RunAsync();
    }
}