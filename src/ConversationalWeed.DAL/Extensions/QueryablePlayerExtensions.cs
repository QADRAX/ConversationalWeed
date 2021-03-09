using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

namespace System.Linq
{
    public static class QueryablePlayerExtensions
    {
        public static IQueryable<Player> IncludeAll(this IQueryable<Player> players)
        {
            return players
                .Include(x => x.PlayerMatches)
                    .ThenInclude(x => x.Match);
        }
    }
}
