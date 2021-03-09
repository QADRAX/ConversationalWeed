using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConversationalWeed.DB.Models
{
    public partial class Player
    {
        public Player()
        {
            PlayerMatches = new HashSet<PlayerMatch>();
            PlayerSkins = new HashSet<PlayerSkin>();
            WinMatches = new HashSet<Match>();
        }

        [Key]
        public ulong Id { get; set; }
        public string Name { get; set; }
        public ulong WeedCoins { get; set; }
        public string? CurrentCardSkin { get; set; }

        public ICollection<PlayerMatch> PlayerMatches { get; set; }

        public ICollection<PlayerSkin> PlayerSkins { get; set; }

        public virtual ICollection<Match> WinMatches { get; set; }

    }
}
