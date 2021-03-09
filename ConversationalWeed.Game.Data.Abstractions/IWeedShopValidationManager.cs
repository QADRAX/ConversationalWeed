using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.Game.Data.Abstractions
{
    public interface IWeedShopValidationManager : IDisposable
    {
        Task<IList<string>> ValidateSetCurrentSkin(ulong playerId, string skinName);
        Task<IList<string>> ValidateBuySkin(ulong playerId, string skinName);
    }
}
