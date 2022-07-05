using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureKeyVaultPractice.API;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, config) =>
{
    var builtConfiguration = config.Build();

    var kvURL = builtConfiguration["KeyVaultConfig:KVUrl"]; 
    var tenantId = builtConfiguration["KeyVaultConfig:TenantId"];
    var clientId = builtConfiguration["KeyVaultConfig:ClientId"];
    var clientSecret = builtConfiguration["KeyVaultConfig:ClientSecretId"];

    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

    var client = new SecretClient(new Uri(kvURL), credential);
    config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
});

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);
app.Run();
