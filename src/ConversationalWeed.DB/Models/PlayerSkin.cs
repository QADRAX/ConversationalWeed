using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConversationalWeed.DB.Models
{
    public partial class PlayerSkin
    {
        public PlayerSkin()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ulong PlayerId { get; set; }

        public string SkinName { get; set; }

        public virtual Player Player { get; set; }
    }
}