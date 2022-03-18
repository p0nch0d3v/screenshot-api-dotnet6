namespace screenshot_api;

public class ScreenshotDTO
{
    public ScreenshotDTO()
    {
    }

    public string Token { get; set; }
    public string Url { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int WaitTime { get; set; }
}