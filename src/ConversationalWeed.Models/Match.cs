using System.Collections.Generic;

namespace ConversationalWeed.Models
{
    public class Match
    {
        public GameType GameType { get; set; }
        public IList<Player> Players { get; set; }
        public IList<Card> Deck { get; set; }
        public IList<Card> Discards { get; set; }
        public int MaxFields { get; set; }
        public int CurrentTurn { get; set; }
        public int CurrentRound { get; set; }
        public bool CurrentPlayerBrick { get; set; }
        public bool GameOver { get; set; }
        public int TotalCards { get; set; }
    }
}
