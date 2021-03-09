using ConversationalWeed.Models;
using System.Collections.Generic;

namespace ConversationalWeed.Game.Abstractions
{
    public interface IValidationManager
    {
        IList<string> ValidateStartRequest(StartMatchRequest request);
        IList<string> ValidatePlayCard(PlayCardRequest request);
        IList<string> ValidateGetInfo(MatchInfoRequest request);
        bool IsPlayerBrick(ulong playerId);
        IList<string> ValidateDiscardCard(DiscardCardRequest request);
    }
}
