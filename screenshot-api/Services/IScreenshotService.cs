using System.Threading.Tasks;

namespace screenshot_api;

public interface IScreenshotService
{
    public Task<byte[]> TakeScreenshot(ScreenshotDTO screenshotRequest);
}