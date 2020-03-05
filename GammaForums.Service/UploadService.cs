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

        public CloudBlobContainer GetBlobContainer(string connectionString)
        {
            return CloudStorageAccount.Parse(connectionString)
            .CreateCloudBlobClient().GetContainerReference("profile-images");
        }
    }
}