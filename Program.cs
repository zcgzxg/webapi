using webapi.Middlewares;
using webapi.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllers();

builder.Services.AddTransient<AppDB>(_ =>
{
    return new AppDB(builder.Configuration["ConnectionStrings:Default"]);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Console.WriteLine(app.Environment.ToString() + "---" + builder.Configuration["ConnectionStrings:Default"]);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseNotfoundMiddleware();

app.Run();
