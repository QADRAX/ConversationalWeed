using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ConversationalWeed.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IMatchRepository MatchRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        IPlayerMatchRepository PlayerMatchRepository { get; }
        IPlayerSkinRepository PlayerSkinRepository { get; }

        IDbContextTransaction BeginTransaction();
        Task SaveChangesAsync();
    }
}
