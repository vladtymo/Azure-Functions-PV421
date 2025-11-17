using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Azure.Storage.Blobs.Models;

public class UploadPhotoFunction
{
    private readonly BlobServiceClient _blobServiceClient;

    public UploadPhotoFunction(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    [Function("UploadPhoto")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        string body = await new StreamReader(req.Body).ReadToEndAsync();
        var form = System.Web.HttpUtility.ParseQueryString(body);

        var file = form["photo"];

        if (file == null)
        {
            return new BadRequestObjectResult("File missing");
        }

        var container = _blobServiceClient.GetBlobContainerClient("images");

        // generate new file name
        string name = Guid.NewGuid().ToString();             // random name
        string extension = Path.GetExtension(file); // get original extension
        string fullName = name + extension;                  // full name: name.ext

        var blob = container.GetBlobClient(fullName);
        await blob.UploadAsync(Stream.Null);

        return new OkObjectResult(new { uploadedUrl = blob.Uri.ToString() });
    }
}
