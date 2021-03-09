using ConversationalWeed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.Game.Abstractions
{
    public interface IActionManager
    {
        Task<MatchResult> CreateMatch(GameType gameType, IList<PlayerRequest> requestedPlayers);
        MatchResult GetMatchResult(ulong playerId);
        MatchResult GameExit(GameExitRequest request);
        Task<MatchResult> PlayCardAsync(PlayCardRequest request);
        Task<MatchResult> DiscardCardAsync(DiscardCardRequest request);
    }
}
