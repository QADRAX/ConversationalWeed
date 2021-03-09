using ConversationalWeed.Infrastructure;
using ConversationalWeed.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

namespace ConversationalWeed.Host
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }

        public async Task RunAsync()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider provider = services.BuildServiceProvider();
            provider.GetRequiredService<LoggingService>();
            provider.GetRequiredService<CommandHandler>();

            await provider.GetRequiredService<StartupService>().StartAsync();
            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var config = new InfrastructureConfig
            {
                Prefix = Configuration["Prefix"],
                DiscordToken = Configuration["DiscordToken"],
                UseFilesInLogs = false,
                DbConnectionString = Configuration["WeedDatabase"]
            };
            services.AddDependencies(config);
        }
    }
}
