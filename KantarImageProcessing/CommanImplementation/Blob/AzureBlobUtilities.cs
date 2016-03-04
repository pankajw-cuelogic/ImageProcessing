using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanImplementation.Blob
{
   internal class AzureBlobUtilities
    {
        public static CloudBlobClient GetBlobClient
        {
            get
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=kantarimagestorage;"
                 + "AccountKey=ayUcPWtQ+kL9j+IKC8HOdngQi3UD3HhP8YCFG721yq8HwooMt1HgmvRRQSniRoCVHhdFVFSzcGXGVi9RgDtPsw==");
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                return blobClient;
            }
        }
    }
}
