namespace ConversationalWeed.Infrastructure
{
    public class InfrastructureConfig
    {
        public string Prefix { get; set; }

        public string DiscordToken { get; set; }

        public bool UseFilesInLogs { get; set; }
        public string DbConnectionString { get; set; }
    }
}
