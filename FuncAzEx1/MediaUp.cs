using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.WebUtilities;
using HttpMultipartParser;

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
        var parser = await MultipartFormDataParser.ParseAsync(req.Body);

        if (parser == null)
            return new BadRequestObjectResult("Invalid data");

        if (parser.Files?.Count == 0)
            return new BadRequestObjectResult("File is missing!");

        var file = parser.Files[0];

        var container = _blobServiceClient.GetBlobContainerClient("images");

        // generate new file name
        string name = Guid.NewGuid().ToString();             // random name
        string extension = Path.GetExtension(file.FileName); // get original extension
        string fullName = name + extension;                  // full name: name.ext

        var blob = container.GetBlobClient(fullName);
        await blob.UploadAsync(file.Data);

        return new OkObjectResult(new { uploadedUrl = blob.Uri.ToString() });
    }
}
