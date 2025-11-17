using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

public class UploadPhotoFunction
{
    private readonly BlobServiceClient _blobServiceClient;

    public UploadPhotoFunction(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    [Function("UploadPhoto")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        //var form = await req.ReadFormAsync();
        //var file = form.Files["photo"];

        //if (file == null)
        //{
        //    var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
        //    await badResponse.WriteStringAsync("File missing");
        //    return badResponse;
        //}

        //var container = _blobServiceClient.GetBlobContainerClient("user-photos");
        //var blob = container.GetBlobClient(Guid.NewGuid().ToString() + ".jpg");

        //using var stream = file.OpenReadStream();
        //await blob.UploadAsync(stream);

        //var response = req.CreateResponse(HttpStatusCode.OK);
        //await response.WriteAsJsonAsync(new { url = blob.Uri.ToString() });

        //return response;
        return new OkObjectResult("Uploaded!");
    }
}
