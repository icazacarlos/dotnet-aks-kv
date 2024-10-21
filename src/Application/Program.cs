using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    // TODO: load variables
}

if (builder.Environment.IsProduction())
{
    /*
    builder.Configuration.AddAzureKeyVault(
        new Uri("https://<Key Vault Name>.vault.azure.net/"),
        new DefaultAzureCredential());
    */

    builder.Configuration.AddAzureKeyVault(
        new Uri("https://<Key Vault Name>.vault.azure.net/"),
        new DefaultAzureCredential(),
        new AzureKeyVaultConfigurationOptions
        {
            ReloadInterval = TimeSpan.FromMinutes(5)
        });

    /*
    builder.Configuration.AddAzureKeyVault(
        new Uri("https://<Key Vault Name>.vault.azure.net/"),
        new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            //ManagedIdentityClientId = builder.Configuration["AzureADManagedIdentityClientId"]
            ManagedIdentityClientId = builder.Configuration["<Managed Identity Client ID>"]
        }));
    */

    /*
    builder.Configuration.AddAzureKeyVault(
        new Uri("https://<Key Vault Name>.vault.azure.net/"),
        new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            //ManagedIdentityClientId = builder.Configuration["AzureADManagedIdentityClientId"]
            ManagedIdentityClientId = builder.Configuration["<Managed Identity Client ID>"]
        }),
        new AzureKeyVaultConfigurationOptions
        {
            ReloadInterval = TimeSpan.FromSeconds(15)
        });
    */
}

var clientId = builder.Configuration["<Secret name in Key Vault>"];

var app = builder.Build();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapGet("/clientId", () =>
{
    return "clientId -> " + clientId;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
