using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Threading.Tasks;

namespace ConversationalWeed.Services
{
    public class CommandHandler
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly string _prefix;

        public CommandHandler(
            IServiceProvider provider,
            DiscordSocketClient discord,
            CommandService commands,
            string prefix)
        {
            _provider = provider;
            _discord = discord;
            _commands = commands;
            _prefix = prefix;

            _discord.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg))
            {
                return;
            }
            if (msg.Author.Id == _discord.CurrentUser.Id)
            {
                return;
            }

            var context = new SocketCommandContext(_discord, msg);

            int argPos = 0;
            if (msg.HasStringPrefix(_prefix, ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                IResult result = await _commands.ExecuteAsync(context, argPos, _provider);

                //if (!result.IsSuccess)
                //{
                //    await context.Channel.SendMessageAsync(result.ToString());
                //}
            }
        }
    }
}
