using System.Collections.Generic;

namespace ConversationalWeed.Models
{
    public class Player
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string CurrentCardSkin { get; set; }
        public int Order { get; set; }
        public int Points { get; set; }
        public IList<Field> Fields { get; set; }
        public IList<Card> Hand { get; set; }
    }
}