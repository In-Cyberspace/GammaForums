using Microsoft.WindowsAzure.Storage.Blob;

namespace Data
{
    public interface IUpload
    {
        CloudBlobContainer GetBlobContainer(string connectionString);
    }
}