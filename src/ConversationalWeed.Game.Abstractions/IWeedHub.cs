using ConversationalWeed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.Game.Abstractions
{
    public interface IWeedHub
    {
        Task<ValidationResult<MatchResult>> StartMatch(StartMatchRequest request);
        ValidationResult<MatchResult> GetMatchInfo(MatchInfoRequest request);
        ValidationResult<MatchResult> GameExit(GameExitRequest request);
        Task<ValidationResult<MatchResult>> PlayCardAsync(PlayCardRequest request);
        Task<ValidationResult<MatchResult>> DiscardCardAsync(DiscardCardRequest request);
        Task<PlayerStats> GetStats(ulong playerId, string playerName);
        Task<IList<CardSkin>> GetPlayerSkins(ulong playerId);
        Task<IList<CardSkin>> GetPurchasableSkins(ulong playerId);
        Task<ValidationResult<CardSkin>> SetCurrentSkin(ulong playerId, string playerName, string skinName);
        Task<ValidationResult<CardSkin>> BuySkin(ulong playerId, string skinName);
    }
}
