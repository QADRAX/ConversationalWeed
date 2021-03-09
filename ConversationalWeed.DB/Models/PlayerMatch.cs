using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConversationalWeed.DB.Models
{
    public partial class PlayerMatch
    {
        public PlayerMatch()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ulong PlayerId { get; set; }

        public int MatchId { get; set; }

        public int SmokedPoints { get; set; }

        public int WeedPoints { get; set; }

        public virtual Player Player { get; set; }

        public virtual Match Match { get; set; }
    }
}