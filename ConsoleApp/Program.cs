﻿using System.Text.Json;
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
    // Set up Weather Client to connect with source data
    //

    using var httpClient = new HttpClient();
    var weatherClient = new WeatherClient(httpClient);

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
        // Fetch forecasts
        //

        var forecasts = await weatherClient.Gridpoint_ForecastAsync(NWSForecastOfficeId.SEW,124,69);

        Console.WriteLine($"OK. Received {forecasts.Properties.Periods.Count} forecasts");

        //
        // Upload logs
        //

        var response = await logsClient.UploadAsync
        (
            logsOptions.DcrImmutableId,
            logsOptions.Stream, 
            [forecasts.Properties.Periods.FirstOrDefault()]
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
