namespace screenshot_api
{
	public static class ConfigurationsManager
	{
		public static string Get_WarminUpUrl(this IConfiguration configuration)
		{
			string envValue = Environment.GetEnvironmentVariable("WARMINUP_URL");

			if (!string.IsNullOrEmpty(envValue))
			{
				return envValue;
			}

			return configuration["WarminUpUrl"];
		}

		public static string Get_Token(this IConfiguration configuration)
		{
			string envValue = Environment.GetEnvironmentVariable("TOKEN");

			if (!string.IsNullOrEmpty(envValue))
			{
				return envValue;
			}

			return configuration["Token"];
		}

		public static string Get_ChromiumExecutablePath(this IConfiguration configuration)
		{
			string envValue = Environment.GetEnvironmentVariable("CHROMIUM_EXECUTABLEPATH");

			if (!string.IsNullOrEmpty(envValue))
			{
				return envValue;
			}

			return configuration["ChromiumExecutablePath"];
		}

		public static string Get_UserAgent(this IConfiguration configuration)
		{
			string envValue = Environment.GetEnvironmentVariable("USER_AGENT");

			if (!string.IsNullOrEmpty(envValue))
			{
				return envValue;
			}

			return configuration["UserAgent"];
		}
	}
}
