using ConversationalWeed.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.Game.Data.Abstractions
{
    public interface IGameDataManager : IDisposable
    {
        Task<PlayerStats> GetPlayerStatsAsync(ulong playerId, string playerName);
        Task<IEnumerable<PlayerStats>> GetLeaderboard();
        Task<IEnumerable<PlayerStats>> RegisterMatch(RegisterMatchRequest request);
        Task AddWeedCoins(ulong playerId, ulong weedCoins);
    }
}
