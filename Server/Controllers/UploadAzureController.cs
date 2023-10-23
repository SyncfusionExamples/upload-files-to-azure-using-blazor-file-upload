using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFileToAzure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadAzureController : ControllerBase
    {
        private readonly string azureConnectionString;
        public UploadAzureController(IConfiguration configuration)
        {
            azureConnectionString = configuration.GetConnectionString("AzureConnectionString");
        }

        [HttpPost("[action]")]
        public async Task Upload(IList<IFormFile> UploadFiles)
        {
            try
            {
                foreach (var files in UploadFiles)
                {
                    // Azure connection string and container name passed as argument to get the Blob reference of container.
                    // Provide your container name as second argument to this method. Example, upload-container.
                    var container = new BlobContainerClient(azureConnectionString, "upload-container");

                    // Method to create our container if it doesn’t exist.
                    var createResponse = await container.CreateIfNotExistsAsync();

                    // If successfully created our container then set a public access type to Blob.
                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                    // Method to create a new Blob client.
                    var blob = container.GetBlobClient(files.FileName);

                    // If the blob with the same name exists, then we delete the blob and its snapshots.
                    await blob.DeleteIfExistsAsync(Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots);

                    // Create a file stream and use the UploadSync method to upload the blob.
                    using (var fileStream = files.OpenReadStream())
                    {
                        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = files.ContentType });
                    }
                }
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
            }
        }
    }
}
