using System.Dynamic;
using System.Text.Json;
using AzLogs.Ingestion;
using AzLogs.Ingestion.Options;
using AzLogs.Ingestion.WeatherServiceTransport;
using Azure.Identity;
using Azure.Monitor.Ingestion;
using Microsoft.Extensions.Configuration;

try
{
    //
    // Load configuration
    //

    var configuration = new ConfigurationBuilder()
        .AddTomlFile("config.toml", optional: true)
        .Build();

    var logsOptions = new LogIngestionOptions();
    configuration.Bind(LogIngestionOptions.Section, logsOptions);

    var idOptions = new IdentityOptions();
    configuration.Bind(IdentityOptions.Section, idOptions);

    //
    // Set up Azure logs ingestion client to connect with logs ingestion endpoint
    //

    var credential = new ClientSecretCredential
    (
        tenantId: idOptions.TenantId.ToString(),
        clientId: idOptions.AppId.ToString(),
        clientSecret: idOptions.AppSecret
    );
    var logsClient = new LogsIngestionClient(logsOptions.EndpointUri, credential);

    //
    // Loop until cancelled
    //

    Console.WriteLine("Press Ctrl-C to exit");

    while(true)
    {
        //
        // Generate messages
        //

        var messages = MessageGenerator.GenerateMessages();
        Console.WriteLine($"OK. Generated {messages.Count} messages");

        //
        // Upload logs
        //

        var response = await logsClient.UploadAsync
        (
            logsOptions.DcrImmutableId,
            logsOptions.Stream, 
            [messages]
        );

        Console.WriteLine($"OK. Uploaded status {response.Status}");

        //
        // Wait
        //

        await Task.Delay(TimeSpan.FromSeconds(5));
    }
}
catch (ApiException<ProblemDetail> ex)
{
    Console.Error.WriteLine("API PROBLEM: {0} {1} {2}", ex.GetType(), ex.Message, JsonSerializer.Serialize(ex.Result));
}
catch (Exception ex)
{
    Console.Error.WriteLine("ERROR: {0} {1}", ex.GetType(), ex.Message);
}
