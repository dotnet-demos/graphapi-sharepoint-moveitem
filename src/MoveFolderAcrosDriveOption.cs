using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace ConsoleApp
{
    class MoveFolderAcrosDriveOption
    {
        GraphServiceClient graphServiceClient;
        ILogger<MoveFolderAcrosDriveOption> logger;
        public MoveFolderAcrosDriveOption(GraphServiceClient graphServiceClient, ILogger<MoveFolderAcrosDriveOption> logger)
        {
            this.graphServiceClient = graphServiceClient;
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>As per the Graph API doc we cannot move between drive using this method.
        /// https://learn.microsoft.com/en-us/graph/api/driveitem-move?view=graph-rest-1.0&tabs=http
        /// </remarks>
        async internal Task Execute()
        {
            logger.LogDebug($"{nameof(MoveFolderAcrosDriveOption)} : Start");

            var sourceDrive = await GetDriveByName("Documents");
            logger.LogInformation($"{nameof(Execute)} - Source Drive 'Documents' found. Id:{sourceDrive.Id}'");
            var sourceFolder = await GetFolderByPath(sourceDrive.Id, "tomove");
            logger.LogInformation($"{nameof(Execute)} - Source folder 'tomove' found. Id:{sourceFolder.Id}'");

            var destinationDrive = await GetDriveByName("Archive", new string[] { "root" });
            logger.LogInformation($"{nameof(Execute)} - Destination Drive 'Archive' found. Id:{sourceDrive.Id}'. Now moving folder");

            sourceFolder.ParentReference = new ItemReference
            {
                DriveId = destinationDrive.Id,
                Id = destinationDrive.Root.Id
            };
            sourceFolder = await graphServiceClient.Drives[sourceDrive.Id].Root.ItemWithPath("tomove").PatchAsync(sourceFolder);

            logger.LogDebug($"{nameof(MoveFolderAcrosDriveOption)} : Moved 'tomove' folder to 'archive' drive. Destination folder id: {sourceFolder.Id}");
        }
        async private Task<DriveItem> GetFolderByPath(string driveId, string path)
        {
            var child = await graphServiceClient.Drives[driveId].Root.ItemWithPath(path).GetAsync();
            return child;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driveName"></param>
        /// <returns></returns>
        /// <exception cref="Exception">When drive not found</exception>
        /// <remarks>As of now Graph API does not support filtering in the request. Get all drives and filter client side.
        /// https://learn.microsoft.com/en-us/graph/api/drive-get?view=graph-rest-1.0&tabs=http
        /// https://learn.microsoft.com/en-us/graph/api/drive-list?view=graph-rest-1.0&tabs=http
        /// </remarks>
        private async Task<Drive> GetDriveByName(string driveName, string[] propertyNamesToExpand = null)
        {
            var dries = await graphServiceClient.Sites["root"].Drives.GetAsync(requestConfiguration => requestConfiguration.QueryParameters.Expand = propertyNamesToExpand);
            return dries.Value.Where(drive => drive.Name.Equals(driveName)).First();
        }
    }
}