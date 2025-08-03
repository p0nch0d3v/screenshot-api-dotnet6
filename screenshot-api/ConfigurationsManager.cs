using Microsoft.Extensions.Options;

namespace screenshot_api
{
	public static class ConfigurationsManager
	{
		public static string Get_WarminUpUrl(this IConfiguration configuration)
		{
			string envValue = Environment.GetEnvironmentVariable("WARMINUP_URL") ?? string.Empty;

			if (!string.IsNullOrEmpty(envValue))
			{
				return envValue;
			}

			return configuration["WarminUpUrl"] ?? string.Empty;
		}

		public static string Get_Token(this IConfiguration configuration)
		{
			string envValue = Environment.GetEnvironmentVariable("TOKEN") ?? string.Empty;

			if (!string.IsNullOrEmpty(envValue))
			{
				return envValue;
			}

			return configuration["Token"] ?? string.Empty;
		}

		public static string Get_ChromiumExecutablePath(this IConfiguration configuration)
		{
			string envValue = Environment.GetEnvironmentVariable("CHROMIUM_EXECUTABLEPATH") ?? string.Empty;

			if (!string.IsNullOrEmpty(envValue))
			{
				return envValue;
			}

			return configuration["ChromiumExecutablePath"] ?? string.Empty;
		}

		public static string Get_UserAgent(this IConfiguration configuration)
		{
			string envValue = Environment.GetEnvironmentVariable("USER_AGENT") ?? string.Empty;

			if (!string.IsNullOrEmpty(envValue))
			{
				return envValue;
			}

			return configuration["UserAgent"] ?? string.Empty;
		}

		public static void ShowConfigurations(this IConfiguration configuration)
		{
			System.Console.WriteLine($"[DEBUG] - WarminUpUrl: {configuration.Get_WarminUpUrl()}");
			System.Console.WriteLine($"[DEBUG] - Token: {configuration.Get_Token()}");
			System.Console.WriteLine($"[DEBUG] - ChromiumExecutablePath: {configuration.Get_ChromiumExecutablePath()}");
			System.Console.WriteLine($"[DEBUG] - UserAgent: {configuration.Get_UserAgent()}");
		}
	}
}
