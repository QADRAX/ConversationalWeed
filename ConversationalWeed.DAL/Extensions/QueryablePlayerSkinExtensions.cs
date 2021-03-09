using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

namespace System.Linq
{
    public static class QueryablePlayerSkinExtensions
    {
        public static IQueryable<PlayerSkin> IncludeAll(this IQueryable<PlayerSkin> playerSkins)
        {
            return playerSkins
                .Include(x => x.Player);
        }
    }
}
