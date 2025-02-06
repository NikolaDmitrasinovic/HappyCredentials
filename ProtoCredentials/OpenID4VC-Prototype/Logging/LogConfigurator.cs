namespace OpenID4VC_Prototype.Logging;

public static class LogConfigurator
{
    public static void Configure()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("../../logs/log.txt", rollingInterval: RollingInterval.Day)
            .Enrich.WithProperty("Application", "OpenID4VC Prototype")
            .CreateLogger();
    }
}
