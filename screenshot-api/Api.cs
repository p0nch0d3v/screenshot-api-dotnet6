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
    }

    private static async Task<IResult> GetHome()
    {
        try
        {
            return Results.NotFound();
            // return Results.Ok(true);
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

    private static string ValidateInput(ScreenshotDTO screenshotRequest)
    {
        string systemToken = Environment.GetEnvironmentVariable("TOKEN");

        if (!string.IsNullOrWhiteSpace(screenshotRequest.Token) && !string.IsNullOrWhiteSpace(systemToken) && string.Compare(screenshotRequest.Token, systemToken, false) != 0) {
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

}