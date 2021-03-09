using ConversationalWeed.DAL.Abstractions.Generic;
using ConversationalWeed.DB.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL.Abstractions
{
    public interface IPlayerMatchRepository :
        IAddRepository<PlayerMatch>,
        IDeleteRepository<PlayerMatch>,
        IUpdateRepository<PlayerMatch>
    {
        Task<IEnumerable<PlayerMatch>> FindAsync(ulong playerId);
    }
}
