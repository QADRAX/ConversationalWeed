using System.Collections.Generic;
using System.Linq;

namespace ConversationalWeed.Models
{
    public static class ListMatchExtensions
    {
        public static Match GetPlayerMatch(this IList<Match> matches, ulong playerId)
        {
            Match result = null;
            foreach (Match match in matches)
            {
                if (match.Players.Where(p => p.Id == playerId).ToList().Count > 0)
                {
                    result = match;
                    break;
                }
            }
            return result;
        }
    }
}
