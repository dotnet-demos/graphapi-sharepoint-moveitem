using EasyConsole;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class MenuService : BackgroundService
    {
        public MoveFolderAcrosDriveOption Option1 { get; init; }
        private readonly TestConnectivityOption testConnectionOption;
        public MenuService(MoveFolderAcrosDriveOption opt1, TestConnectivityOption testConnectionOption)
        {
            Option1 = opt1;
            this.testConnectionOption = testConnectionOption;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var menu = new Menu()
                .Add("Test connectivity", async (token) => await testConnectionOption.Execute())
                .Add("Move folder to another drive", async (token) => await Option1.Execute())
                .AddSync("Exit", () => Environment.Exit(0));
            await menu.Display(CancellationToken.None);
            await base.StartAsync(stoppingToken);
        }
    }
}