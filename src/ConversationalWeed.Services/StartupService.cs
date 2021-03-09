using ConversationalWeed.DB.Models;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ConversationalWeed.Services
{
    public class StartupService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly string _discordToken;

        public StartupService(
            IServiceProvider provider,
            DiscordSocketClient discord,
            CommandService commands,
            string discordToken)
        {
            _provider = provider;
            _discordToken = discordToken;
            _discord = discord;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            using(IServiceScope serviceScope = _provider.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;
                WeedLeaderboardContext context = services.GetService<WeedLeaderboardContext>();
                context.Database.Migrate();
            }

            await _discord.LoginAsync(TokenType.Bot, _discordToken);
            await _discord.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), _provider);
        }
    }
}
