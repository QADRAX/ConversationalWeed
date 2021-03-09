using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DAL.Generic;
using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL
{
    public class PlayerMatchRepository : GenericRepository<PlayerMatch>, IPlayerMatchRepository
    {
        public PlayerMatchRepository(WeedLeaderboardContext context) : base(context) { }

        #region Public Methods

        public async Task<IEnumerable<PlayerMatch>> FindAsync(ulong playerId)
        {
            IEnumerable<PlayerMatch> result = await base.GetAll()
                .IncludeAll()
                .Where(x => x.PlayerId == playerId)
                .ToListAsync();
            return result;
        }

        #endregion
    }
}
