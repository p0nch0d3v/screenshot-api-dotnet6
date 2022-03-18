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

    private static async Task<IResult> TakeScreenshot(ScreenshotDTO screenshot, IScreenshotService service)
    {
        try
        {
            if (ValidateToken(screenshot.Token))
            {

            }
            else 
            {
                return Results.Problem("Not valid token");
            }
            
            return Results.Ok(true);
        }
        catch (System.Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static bool ValidateToken(string token)
    {
        string systemToken = Environment.GetEnvironmentVariable("TOKEN");
        return string.Compare(token, systemToken, false) == 0;
    }

}