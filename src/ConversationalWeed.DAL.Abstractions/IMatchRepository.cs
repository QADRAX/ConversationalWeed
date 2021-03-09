using ConversationalWeed.DAL.Abstractions.Generic;
using ConversationalWeed.DB.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL.Abstractions
{
    public interface IMatchRepository :
        IAddRepository<Match>,
        IDeleteRepository<Match>,
        IUpdateRepository<Match>
    {
        Task<IEnumerable<Match>> GetAllAsync();

        Task<Match> FindAsync(int id);
    }
}
