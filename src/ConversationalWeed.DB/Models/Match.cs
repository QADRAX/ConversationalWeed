using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConversationalWeed.DB.Models
{
    public partial class Match
    {
        public Match()
        {
            PlayerMatches = new HashSet<PlayerMatch>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime FinishedAtUtc { get; set; }

        public ulong? WinnerId { get; set; }

        public virtual Player Winner { get; set; }

        public ICollection<PlayerMatch> PlayerMatches { get; set; }

    }
}