using Tommy;

namespace Foxhole_Core
{
	public static class Config
	{
		public static string Local_URL { get; set; } = "https://127.0.0.1:5000";
	}


	// Startup class needed to connect to the Discord bot
	class Startup
	{
		readonly public TomlTable config;

		// Function to create a TOML configuration file if the latter one was invalid.
		private static TomlTable CreateNewConfig()
		{
			static string RequestConfigParameter(string message)
			{
				Console.WriteLine(message);
				string? input;

				do { input = Console.ReadLine(); }

				while (string.IsNullOrEmpty(input));
				
				return input;
			}


			if (RequestConfigParameter("\nWould you like to create a new configuration file? (Y/N)").ToLower() == "n")
				Environment.Exit(-1);

			using StreamWriter writer = File.CreateText("Config.toml");
			TomlTable new_config = new()
			{
				["LocalURL"] = RequestConfigParameter("\nPlease insert application's locally hosted URL. (Example: https://0.0.0.0:443;http://0.0.0.0:80)")
			};

			new_config.WriteTo(writer);
			writer.Flush();
			Console.WriteLine("Created \"Config.toml\"!\n" +
				"Some more options are available for manual edit. (Remember to delete the file if something goes wrong).");
			return new_config;
		}

		public Startup(IConfiguration configuration)
		{
			try
			{
				using StreamReader reader = File.OpenText("Config.toml");
				config = TOML.Parse(reader);

			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("Configuration file not found...");
				config = CreateNewConfig();
			}
			catch (TomlSyntaxException)
			{
				Console.WriteLine("Configuration file missing arguments or improperly formated...");
				config = CreateNewConfig();
			}


			Config.Local_URL = config["LocalURL"];

			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddRazorPages();
		}

		public void WebHost(ConfigureWebHostBuilder builder)
		{
			builder.UseUrls(Config.Local_URL);
		}


		public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
		{
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
				Path.Combine(Directory.GetCurrentDirectory())),
				RequestPath = "/wwwroot"
			});

			environment.ApplicationName = "Foxhole Core";
		}
	}
}