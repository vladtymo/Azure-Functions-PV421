using Azure.Storage.Blobs;
using FuncAzEx1.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton(x =>
            new BlobServiceClient(Environment.GetEnvironmentVariable("BLOB_CONNECTION")));
        services.AddScoped<IEmailSender, EmailSender>();
    })
    .Build();

host.Run();
