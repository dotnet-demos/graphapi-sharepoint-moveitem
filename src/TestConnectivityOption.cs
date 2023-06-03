using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class TestConnectivityOption
    {
        ILogger<TestConnectivityOption> logger;
        GraphServiceClient graphServiceClient;
        public TestConnectivityOption(ILogger<TestConnectivityOption> logger, GraphServiceClient graphServiceClient)
        {
            this.logger = logger;
            this.graphServiceClient = graphServiceClient;
        }
        async internal Task Execute()
        {
            logger.LogDebug($"{nameof(MoveFolderAcrosDriveOption)} : Start");
            var site = await graphServiceClient.Sites["root"].GetAsync();
            logger.LogInformation($"{nameof(Execute)} - Root site Id:{site.Id}");
            logger.LogDebug($"{nameof(MoveFolderAcrosDriveOption)} : Completed");
        }
    }
}