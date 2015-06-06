using RedStar.Invoicing.Models;
using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Framework.ConfigurationModel;

namespace RedStar.Invoicing.Commands
{
    public class SaveSettingsCommand
    {
        public async void Execute(SettingsDTO settingsDto)
        {
            // TODO: image type and size
            var imageBytes = Convert.FromBase64String(settingsDto.Logo.Substring(settingsDto.Logo.IndexOf(",") + 1));
            var imageExtension = settingsDto.LogoName.Substring(settingsDto.LogoName.LastIndexOf("."));

            var configuration = new Configuration().AddUserSecrets();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.Get("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("icons");

            // TODO: only do once?
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var blockBlob = container.GetBlockBlobReference(Guid.NewGuid().ToString().Replace("-", "") + imageExtension);

            await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
        }
    }
}
