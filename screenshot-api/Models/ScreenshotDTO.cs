namespace screenshot_api;

public class ScreenshotDTO
{
    public ScreenshotDTO()
    {
    }

    public string Token { get; set; }
    public string Url { get; set; }
    public uint? Width { get; set; }
    public uint? Height { get; set; }
    public uint? WaitTime { get; set; }
    public bool FullPage { get; set; } = false;
}

