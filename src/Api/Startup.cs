namespace Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment environment)
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.Develipment.json")
            .Build();
    }

}

