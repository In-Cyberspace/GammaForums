using Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GammaForums.Service
{
    public class UploadService : IUpload
    {
        public UploadService()
        {
        }

        public CloudBlobContainer GetBlobContainer(string connectionString, string connectionName)
        {
            return CloudStorageAccount.Parse(connectionString)
            .CreateCloudBlobClient().GetContainerReference(connectionName);
        }
    }
}