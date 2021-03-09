using ConversationalWeed.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationalWeed.Game.Data.Abstractions
{
    public interface IWeedShopManager : IDisposable
    {
        Task<CardSkin> GetPlayerCurrentSkin(ulong playerId, string playerName);
        Task<IList<CardSkin>> GetPlayerSkins(ulong playerId);
        Task<IList<CardSkin>> GetPursachableSkins(ulong playerId);
        Task<CardSkin> SetPlayerCurrentSkin(ulong playerId, string playerName, string skinName);
        Task<CardSkin> BuySkin(ulong playerId, string skinName);
    }
}
