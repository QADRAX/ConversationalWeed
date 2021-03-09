using ConversationalWeed.DAL;
using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DB.Models;
using ConversationalWeed.Game;
using ConversationalWeed.Game.Abstractions;
using ConversationalWeed.Game.Data;
using ConversationalWeed.Game.Data.Abstractions;
using ConversationalWeed.Game.Pictures;
using ConversationalWeed.Game.Pictures.Abstractions;
using ConversationalWeed.Infrastructure;
using ConversationalWeed.Services;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        #region Public Methods

        public static IServiceCollection AddDependencies(this IServiceCollection services, InfrastructureConfig config)
        {
            services
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    MessageCacheSize = 1000
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    DefaultRunMode = RunMode.Async,
                }))
                .AddSingleton(s => new CommandHandler(s, s.GetRequiredService<DiscordSocketClient>(), s.GetRequiredService<CommandService>(), config.Prefix))
                .AddSingleton(s => new StartupService(s, s.GetRequiredService<DiscordSocketClient>(), s.GetRequiredService<CommandService>(), config.DiscordToken))
                .AddSingleton(s => new LoggingService(s.GetRequiredService<DiscordSocketClient>(), s.GetRequiredService<CommandService>(), config.UseFilesInLogs))
                .AddSingleton<IWeedHub, WeedHub>()
                .AddSingleton<IActionManager, ActionManager>()
                .AddSingleton<IValidationManager, ValidationManager>()
                .AddSingleton<WeedContext>()
                .AddSingleton<IImageGenerator, ImageGenerator>()
                .AddTransient<IMatchRepository, MatchRepository>()
                .AddTransient<IPlayerMatchRepository, PlayerMatchRepository>()
                .AddTransient<IPlayerRepository, PlayerRepository>()
                .AddTransient<IPlayerSkinRepository, PlayerSkinRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IGameDataManager, GameDataManager>()
                .AddTransient<IWeedShopManager, WeedShopManager>()
                .AddTransient<IWeedShopValidationManager, WeedShopValidationManager>()
                .AddDbContext<WeedLeaderboardContext>(options =>
                {
                    options.UseMySql(config.DbConnectionString);
                    options.EnableSensitiveDataLogging();
                });

            return services;
        }

        #endregion
    }
}
