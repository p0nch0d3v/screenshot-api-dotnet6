using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace screenshot_api;

public static class Api
{
    public static void ConfigureAPI(this WebApplication app)
    {
        // All API endpoint mapping
        app.MapGet("/", ()=> Results.NotFound() );
        app.MapPost("/screenshot", TakeScreenshot);
        app.MapGet("/healtcheck", HealthCheck);
    }

    private static async Task<IResult> GetHome()
    {
        try
        {
            return Results.NotFound();
        }
        catch (System.Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> TakeScreenshot(ScreenshotDTO screenshotRequest, IScreenshotService service)
    {
        try
        {
            string validation = ValidateInput(screenshotRequest);
            if (!string.IsNullOrWhiteSpace(validation))
            {
                return Results.Problem(validation);
            }
            var screenshot = await service.TakeScreenshot(screenshotRequest);
            return Results.File(fileContents: screenshot, 
                contentType: "image/jpeg", 
                fileDownloadName: "image.jpeg"
            );
        }
        catch (System.Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> HealthCheck(string token)
    {
        if (isTokenValid(token))
        {
            return Results.Ok($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
        }
        return Results.Unauthorized();
    }

    private static string ValidateInput(ScreenshotDTO screenshotRequest)
    {
        if (!isTokenValid(screenshotRequest.Token))
        {
            return "Invalid Token";
        }

        if (string.IsNullOrWhiteSpace(screenshotRequest.Url))
        {
            return "Invalid URL";
        }

        if (screenshotRequest.Width.HasValue && screenshotRequest.Width <= 0) 
        {
            return "Invalid Width";
        }

        if (screenshotRequest.Height.HasValue && screenshotRequest.Height <= 0) 
        {
            return "Invalid Height";
        }

        return null;
    }

    private static bool isTokenValid(string token)
    {
        string systemToken = Environment.GetEnvironmentVariable("TOKEN");

        return (string.IsNullOrWhiteSpace(token) 
            || string.IsNullOrWhiteSpace(systemToken)
            || string.Compare(token, systemToken, false) == 0);
    }
}

