using System;
using System.Collections.Generic;

namespace ConversationalWeed.Models
{
    public class RegisterMatchRequest
    {
        public DateTime FinishedAtUtc { get; set; }
        public IList<RegisterPlayerMatchRequest> Players { get; set; }
        public ulong? WinnerId { get; set; }
    }

    public class RegisterPlayerMatchRequest
    {
        public ulong PlayerId { get; set; }
        public string Name { get; set; }
        public int SmokedPoints { get; set; }
        public int WeedPoints { get; set; }
    }
}
