using System.Collections.Generic;

namespace ConversationalWeed.Models
{
    public class MatchResult
    {
        public IList<Player> Players { get; set; }
        public int DeckSize { get; set; }
        public int Turn { get; set; }
        public int Round { get; set; }
        public bool IsCurrentPlayerBrick { get; set; }
        public bool GameOver { get; set; }
        public Player CurrentPlayer { get; set; }
        public int RoundsLeft { get; set; }
        public IList<PlayerStats> PlayerStats { get; set; }
    }
}
