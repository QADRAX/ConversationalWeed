using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DAL.Generic;
using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL
{
    public class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        #region Constructor

        public MatchRepository(WeedLeaderboardContext context) : base(context) { }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            IEnumerable<Match> result = await base.GetAll()
                .IncludeAll()
                .ToListAsync();
            return result;
        }

        public async Task<Match> FindAsync(int id)
        {
            Match result = await base.GetAll()
                .IncludeAll()
                .SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }

        #endregion
    }
}
