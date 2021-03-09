using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

namespace System.Linq
{
    public static class QueryablePlayerMatchExtensions
    {
        public static IQueryable<PlayerMatch> IncludeAll(this IQueryable<PlayerMatch> playerMatches)
        {
            return playerMatches
                .Include(x => x.Player)
                .Include(x => x.Match);
        }
    }
}
