using DallENet;
using DallENet.Models;
using DallENet.ServiceConfigurations;
using SustainaBiteAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ChatRepository>();
builder.Services.AddTransient<ImageGeneratorRepository>();

builder.Services.AddDallE(options =>
{
    // Azure OpenAI Service.
    options.UseAzure(
        resourceName: builder.Configuration.GetValue<string>("AzureOpenAI:DallE:ResourceName"),
        apiKey: builder.Configuration.GetValue<string>("AzureOpenAI:DallE:Key"),
        authenticationType: AzureAuthenticationType.ApiKey,
        apiVersion: builder.Configuration.GetValue<string>("AzureOpenAI:DallE:ApiVersion")
    );

    options.DefaultResolution = DallEImageResolutions.Medium;     // Default: Large (1024x1024)
    //options.DefaultImageCount = 2;  // Default: 1
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public record class Request(string Prompt);
