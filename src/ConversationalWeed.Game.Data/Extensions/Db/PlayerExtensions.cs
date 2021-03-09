using ConversationalWeed.Models;
using ConversationalWeed.Game.Data.Constants;

using System.Linq;

namespace ConversationalWeed.DB.Models
{
    public static class PlayerExtensions
    {
        public static PlayerStats ToPlayerStats(this Player player)
        {
            var points = player.PlayerMatches
                .GroupBy(o => o.PlayerId)
                .Select(g => new { TotalSmoked = g.Sum(x => x.SmokedPoints), TotalWeedPoints = g.Sum(x => x.WeedPoints) })
                .FirstOrDefault();

            var wins = player.PlayerMatches.Where(x => x.Match.WinnerId == player.Id).ToList();

            var currentSkin = player.CurrentCardSkin != null 
                ? player.CurrentCardSkin 
                : PurchasableItems.DEFAULT_SKIN_NAME;

            return new PlayerStats
            {
                PlayerId = player.Id,
                Name = player.Name,
                TotalMatches = player.PlayerMatches.Count,
                TotalWins = wins.Count,
                TotalSmokedPoints = points != null ? points.TotalSmoked : 0,
                TotalWeedPoints = points != null ? points.TotalWeedPoints : 0,
                WeedCoins = player.WeedCoins,
                CurrentSkin = currentSkin,
            };
        }
    }
}
