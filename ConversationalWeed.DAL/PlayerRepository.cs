using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DAL.Generic;
using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(WeedLeaderboardContext context) : base(context) { }

        #region Public Methods

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            IEnumerable<Player> result = await base.GetAll()
                .IncludeAll()
                .ToListAsync();
            return result;
        }

        public async Task<Player> FindAsync(ulong id)
        {
            Player result = await base.GetAll()
                .IncludeAll()
                .SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }

        #endregion
    }
}
