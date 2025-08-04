using Microsoft.OpenApi.Models;
using screenshot_api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
	options.Limits.MaxConcurrentConnections = 100;
	options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
  options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);
});

// Add services to the container.
builder.Services.AddScoped<IScreenshotService, ScreenshotService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
	{
		c.SwaggerDoc("v1", new OpenApiInfo
		{
			Title = "Screenshot API",
			Version = "v1"
		});
	});

WebApplication app = builder.Build();
app.Configuration.ShowConfigurations();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureAPI();

// Warming up
string warming_url = app.Configuration.Get_WarminUpUrl();
if (!string.IsNullOrWhiteSpace(warming_url))
{
	using (IServiceScope serviceScope = app.Services.CreateScope())
	{
		IScreenshotService screenshotService = serviceScope.ServiceProvider.GetService<IScreenshotService>();

		await screenshotService.TakeScreenshot(
			new ScreenshotDTO()
			{
				Url = warming_url,
				Width = 800,
				Height = 600,
				WaitTime = 1, 
				FullPage = false
			}
		);
	}
}
else
{
	System.Console.WriteLine("[LOG] - Skipping Warm Up");
}

app.Run();
