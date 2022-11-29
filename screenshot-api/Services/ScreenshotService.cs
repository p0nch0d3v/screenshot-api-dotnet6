using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace screenshot_api;

public class ScreenshotService : IScreenshotService
{
    public async Task<byte[]> TakeScreenshot(ScreenshotDTO screenshotRequest)
    {
        System.Console.WriteLine($"[LOG] - Taking Screenshot, Url: {screenshotRequest.Url}, Width: {screenshotRequest.Width}, Height: {screenshotRequest.Height}, WaitTime: {screenshotRequest.WaitTime}, FullPage: {screenshotRequest.FullPage}");
        
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
                ExecutablePath = Environment.GetEnvironmentVariable("CHROMIUM_EXECUTABLEPATH"),
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
            using (Page page = await browser.NewPageAsync())
            {
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("USER_AGENT")))
                {
                    await page.SetUserAgentAsync(Environment.GetEnvironmentVariable("USER_AGENT"));
                }
                await page.GoToAsync(screenshotRequest.Url);
                
                if (screenshotRequest.WaitTime.HasValue) 
                {
                    Task.Delay((int)screenshotRequest.WaitTime * 1000).Wait();
                }
                
                result = await page.ScreenshotDataAsync(new ScreenshotOptions() { 
                    FullPage = screenshotRequest.FullPage, 
                    Type = ScreenshotType.Jpeg,
                    Quality = 100,
                });
            }
        }
        System.Console.WriteLine("[LOG] - Taking Screenshot Done");
        return result;
    }
}

