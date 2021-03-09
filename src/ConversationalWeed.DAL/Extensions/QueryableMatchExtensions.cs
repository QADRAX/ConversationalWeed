using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

namespace System.Linq
{
    public static class QueryableMatchExtensions
    {
        public static IQueryable<Match> IncludeAll(this IQueryable<Match> matches)
        {
            return matches.Include(x => x.Winner);
        }
    }
}
