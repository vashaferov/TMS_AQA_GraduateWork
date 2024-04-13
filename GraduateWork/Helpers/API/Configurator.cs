using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace GraduateWork.Helpers.API;

public class Configurator
{
    private static readonly Lazy<IConfiguration> s_configuration;
    public static IConfiguration Configuration => s_configuration.Value;

    static Configurator()
    {
        s_configuration = new Lazy<IConfiguration>(BuildConfiguration);
    }

    private static IConfiguration BuildConfiguration()
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath ?? throw new InvalidOperationException())
            .AddJsonFile("appsettings.json");

        var appSettingFiles = Directory.EnumerateFiles(basePath ?? string.Empty, "appsettings.*.json");

        foreach (var appSettingFile in appSettingFiles)
        {
            builder.AddJsonFile(appSettingFile);
        }

        return builder.Build();
    }

    public static AppSettings AppSettings
    {
        get
        {
            var appSettings = new AppSettings();
            var child = Configuration.GetSection("AppSettings");

            appSettings.URL = child["URL"];
            appSettings.API_URL = child["API_URL"];
            appSettings.Username = child["Username"];
            appSettings.Password = child["Password"];
            appSettings.ApiKey = child["ApiKey"];

            return appSettings;
        }
    }

    public static string? BrowserType => Configuration[nameof(BrowserType)];

    public static double WaitsTimeout => Double.Parse(Configuration[nameof(WaitsTimeout)]);
}