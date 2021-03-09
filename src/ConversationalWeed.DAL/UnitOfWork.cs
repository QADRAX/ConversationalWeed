using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore.Storage;

using System.Threading.Tasks;

namespace ConversationalWeed.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WeedLeaderboardContext context;

        public IMatchRepository MatchRepository { get; private set; }

        public IPlayerMatchRepository PlayerMatchRepository { get; private set; }

        public IPlayerRepository PlayerRepository { get; private set; }

        public IPlayerSkinRepository PlayerSkinRepository { get; private set; }

        public UnitOfWork(WeedLeaderboardContext context,
            IMatchRepository matchRepository,
            IPlayerMatchRepository playerMatchRepository,
            IPlayerRepository playerRepository,
            IPlayerSkinRepository playerSkinRepository)
        {
            this.context = context;
            MatchRepository = matchRepository;
            PlayerMatchRepository = playerMatchRepository;
            PlayerRepository = playerRepository;
            PlayerSkinRepository = playerSkinRepository;
        }

        public IDbContextTransaction BeginTransaction() => context.Database.BeginTransaction();

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();
        public void Dispose() => context.Dispose();
    }
}
