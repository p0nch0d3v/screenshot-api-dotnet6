using System.Threading.Tasks;
using PuppeteerSharp;

namespace screenshot_api;

public class ScreenshotService : IScreenshotService
{
    public async Task<byte[]> TakeScreenshot(ScreenshotDTO screenshotRequest)
    {
        System.Console.WriteLine("Take Screenshot, Url: " + screenshotRequest.Url);
        byte[] result = null;
        var options = new LaunchOptions
            {
                Headless = true,
                Args = new[]
                {
                    "--no-sandbox",
                    "--disable-gpu",
                    "--disable-setuid-sandbox", 
                    "--enable-logging", 
                    "--v=1"
                },
                ExecutablePath = "/usr/bin/chromium",
                DefaultViewport = new ViewPortOptions
                {
                    Width = screenshotRequest.Width.HasValue ? (int)screenshotRequest.Width : 1920,
                    Height = screenshotRequest.Height.HasValue ? (int)screenshotRequest.Height : 1080
                }
            };
        using var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        
        using (var browser = await Puppeteer.LaunchAsync(options)) 
        {
            using (var page = await browser.NewPageAsync())
            {
                await page.GoToAsync(screenshotRequest.Url);
                
                if (screenshotRequest.WaitTime.HasValue) 
                {
                    Task.Delay((int)screenshotRequest.WaitTime).Wait();
                }
                
                result = await page.ScreenshotDataAsync(new ScreenshotOptions() { 
                    FullPage = true, 
                    Type = ScreenshotType.Jpeg,
                    Quality = 100
                });
            }
        }

        return result;
    }
}

