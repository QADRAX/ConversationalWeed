using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DAL.Generic;
using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL
{
    public class PlayerSkinRepository : GenericRepository<PlayerSkin>, IPlayerSkinRepository
    {
        public PlayerSkinRepository(WeedLeaderboardContext context) : base(context) { }

        #region Public Methods

        public async Task<IEnumerable<PlayerSkin>> FindAsync(ulong playerId)
        {
            IEnumerable<PlayerSkin> result = await base.GetAll()
                .IncludeAll()
                .Where(x => x.PlayerId == playerId)
                .ToListAsync();
            return result;
        }

        #endregion
    }
}
