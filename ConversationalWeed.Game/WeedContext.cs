using ConversationalWeed.Models;
using System.Collections.Generic;

namespace ConversationalWeed.Game
{
    public class WeedContext
    {
        public IList<Match> Matches { get; set; }

        public WeedContext()
        {
            Matches = new List<Match>();
        }
    }
}
