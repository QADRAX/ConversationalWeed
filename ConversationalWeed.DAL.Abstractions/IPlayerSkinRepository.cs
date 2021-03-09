using ConversationalWeed.DAL.Abstractions.Generic;
using ConversationalWeed.DB.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL.Abstractions
{
    public interface IPlayerSkinRepository :
        IAddRepository<PlayerSkin>,
        IDeleteRepository<PlayerSkin>,
        IUpdateRepository<PlayerSkin>
    {
        Task<IEnumerable<PlayerSkin>> FindAsync(ulong playerId);
    }
}
