using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System;
using System.IO;
using System.Threading.Tasks;

namespace ConversationalWeed.Services
{
    public class LoggingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly bool _useFilesInLogs;
        private string _logDirectory { get; }
        private string _logFile => Path.Combine(_logDirectory, $"{DateTime.UtcNow:yyyy-MM-dd}.txt");

        public LoggingService(DiscordSocketClient discord, CommandService commands, bool useFilesInLogs)
        {
            _useFilesInLogs = useFilesInLogs;
            _logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");

            _discord = discord;
            _commands = commands;

            _discord.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        private Task OnLogAsync(LogMessage msg)
        {
            string logText = $"{DateTime.UtcNow:hh:mm:ss} [{msg.Severity}] {msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";
            if (_useFilesInLogs)
            {
                WriteFile(logText);
            }
            return Console.Out.WriteLineAsync(logText);
        }

        private void WriteFile(string logText)
        {
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            if (!File.Exists(_logFile))
            {
                File.Create(_logFile).Dispose();
            }

            File.AppendAllText(_logFile, logText + "\n");
        }
    }
}
