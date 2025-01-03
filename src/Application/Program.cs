using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUri = builder.Configuration["KEY_VAULT_URI"];
var secretName = builder.Configuration["SECRET_NAME"];

builder.Configuration.AddAzureKeyVault(
    new Uri(keyVaultUri),
    new DefaultAzureCredential());

var secretValue = builder.Configuration[secretName];

var app = builder.Build();

app.MapGet("/getSecret", () =>
{
    return $"  {secretName}: " + secretValue;
});

app.Run();
