using System.Threading.Tasks;

namespace ConversationalWeed.Host
{
    class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
