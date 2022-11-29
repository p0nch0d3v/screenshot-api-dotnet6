using Microsoft.OpenApi.Models;
using screenshot_api;

IScreenshotService screenshotService = new ScreenshotService();
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
});

// Add services to the container.
builder.Services.AddScoped<IScreenshotService, ScreenshotService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { 
            Title = "Todo API", 
            Version = "v1" 
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureAPI();

// Warming up
string warming_url = Environment.GetEnvironmentVariable("WARMINUP_URL");
if (!string.IsNullOrWhiteSpace(warming_url)) {
    await screenshotService.TakeScreenshot(
        new ScreenshotDTO(){ 
            Url = warming_url, 
            Width = 800, 
            Height = 600 
        }
    );
}
else {
    System.Console.WriteLine("[LOG] - Skipping Warm Up");
}

app.Run();

