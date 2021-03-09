using System;
using System.Collections.Generic;
using System.Text;

namespace ConversationalWeed.Models
{
    public class StartMatchRequest
    {
        public IList<PlayerRequest> Players { get; set; }
        public GameType Type { get; set; }
    }

    public class PlayerRequest
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
    }
}
