using webapi.Database;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.ResponseCompression;
using webapi.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
    });

builder.UseRelationalDB(builder.Configuration["ConnectionStrings:Mysql"]);
builder.Services.AddRedis(builder.Configuration["ConnectionStrings:Redis"]);
builder.UseMemoryCache();

builder.Services.AddResponseCompression(option =>
{
    option.Providers.Add<GzipCompressionProvider>();
    option.Providers.Add<BrotliCompressionProvider>();
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerGeneratorOptions = new Swashbuckle.AspNetCore.SwaggerGen.SwaggerGeneratorOptions() { DescribeAllParametersInCamelCase = false };
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WEB API",
        Description = "An ASP.NET Core Web API",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{builder.Environment.ApplicationName}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseResponseCompression();
app.MapControllers();

app.Run();
