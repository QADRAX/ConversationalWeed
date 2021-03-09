using ConversationalWeed.DAL.Abstractions.Generic;
using ConversationalWeed.DB.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL.Abstractions
{
    public interface IPlayerRepository :
        IAddRepository<Player>,
        IDeleteRepository<Player>,
        IUpdateRepository<Player>
    {
        Task<IEnumerable<Player>> GetAllAsync();

        Task<Player> FindAsync(ulong id);
    }
}
