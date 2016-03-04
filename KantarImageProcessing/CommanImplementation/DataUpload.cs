using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using CommanImplementation.Blob;
using System.Drawing;

namespace CommanImplementation
{
    public class DataUpload
    {
        /// <summary>
        /// Upload file to blob storage
        /// </summary>
        /// <param name="filePath"></param>
        public async void UploadFile(string filePath)
        { 
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);
                CustomFile file = new CustomFile();
                file.FileName = filePath.Contains("\\") ? filePath.Split('\\')[filePath.Split('\\').Count() - 1] : filePath;
                file.FileMime = "image/jpg";
                file.FileBytes = ms.ToArray();

                await UploadFileToBlob(file);
            }
        }

        /// <summary>
        /// Upload file to blob storage. input Custome file object
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> UploadFileToBlob(CustomFile file)
        {
            // Get Blob Container
            CloudBlobContainer _container = AzureBlobUtilities.GetBlobClient.GetContainerReference("kantarimages");
            _container.CreateIfNotExists();

            // Get reference to blob (binary content)
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(file.FileName);

            // set its properties
            blockBlob.Properties.ContentType = file.FileMime;
            blockBlob.Metadata["filename"] = file.FileName;
            blockBlob.Metadata["filemime"] = file.FileMime;

            // Get stream from file bytes
            Stream stream = new MemoryStream(file.FileBytes);

            // Async upload of stream to Storage
            AsyncCallback uploadCompleted = new AsyncCallback(OnUploadCompleted);
            blockBlob.BeginUploadFromStream(stream, uploadCompleted, blockBlob);

            return true;
        }

        /// <summary>
        /// Event will occure when download complated
        /// </summary>
        /// <param name="result"></param>
        private void OnUploadCompleted(IAsyncResult result)
        {
            CloudBlockBlob blob = (CloudBlockBlob)result.AsyncState;
            blob.SetMetadata();
            blob.EndUploadFromStream(result);
        }

        /// <summary>
        /// Download file from blob storage
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public  byte[] DownloadFileFromBlob(string fileName)
        {
            // Get Blob Container
            CloudBlobContainer _container = AzureBlobUtilities.GetBlobClient.GetContainerReference("kantarimages");
            // Get reference to blob (binary content)
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(fileName);
            // Read content
            using (MemoryStream ms = new MemoryStream())
            {
                blockBlob.DownloadToStream(ms);
                return ms.ToArray();
            }
        }
    }
}
