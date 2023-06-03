using EasyConsole;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class MenuService : BackgroundService
    {
        public MoveFolderAcrosDriveOption Option1 { get; init; }
        private readonly TestConnectivityOption testConnectionOption;
        private readonly ILogger<MenuService> logger;
        public MenuService(MoveFolderAcrosDriveOption opt1, TestConnectivityOption testConnectionOption,ILogger<MenuService> logger)
        {
            Option1 = opt1;
            this.testConnectionOption = testConnectionOption;
            this.logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var menu = new Menu()
                .Add("Test connectivity", async (token) => await testConnectionOption.Execute())
                .Add("Move folder to another drive", async (token) => await Option1.Execute())
                .AddSync("Exit", () => Environment.Exit(0));
            try
            {
                await menu.Display(CancellationToken.None);
            }catch(Exception ex)
            {
                logger.LogError(ex,"Exception occured");
            }
            await base.StartAsync(stoppingToken);
        }
    }
}