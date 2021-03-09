using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DB.Models;
using ConversationalWeed.Game.Data.Abstractions;
using ConversationalWeed.Game.Data.Constants;
using ConversationalWeed.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbPlayer = ConversationalWeed.DB.Models.Player;

namespace ConversationalWeed.Game.Data
{
    public class WeedShopManager : DataManager, IWeedShopManager
    {
        public WeedShopManager(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region Public methods

        public async Task<CardSkin> GetPlayerCurrentSkin(ulong playerId, string playerName)
        {
            CardSkin result = PurchasableItems.DEFAULT_SKIN;

            DbPlayer dbPlayer = await GetOrCreatePlayer(playerId, playerName);
            string currentCardSkin = dbPlayer.CurrentCardSkin;

            if (!string.IsNullOrWhiteSpace(currentCardSkin))
            {
                CardSkin existingSkin = PurchasableItems.Skins.FirstOrDefault(x => x.Name == currentCardSkin);
                if (existingSkin != null)
                {
                    result = existingSkin;
                }
            }
            return result;
        }

        public async Task<IList<CardSkin>> GetPlayerSkins(ulong playerId)
        {
            IEnumerable<PlayerSkin> playerSkins = await unitOfWork.PlayerSkinRepository.FindAsync(playerId);

            IList<CardSkin> skins = playerSkins.Select(x => x.ToCardSkin()).ToList();
            skins.Add(PurchasableItems.DEFAULT_SKIN);

            return skins;
        }

        public async Task<IList<CardSkin>> GetPursachableSkins(ulong playerId)
        {
            IList<CardSkin> skins = await GetPlayerSkins(playerId);
            IList<CardSkin> pursachableSkins = PurchasableItems.Skins.Where(x => !skins.Contains(x)).ToList();

            return pursachableSkins;
        }

        public async Task<CardSkin> SetPlayerCurrentSkin(ulong playerId, string playerName, string skinName)
        {
            DbPlayer dbPlayer = await GetOrCreatePlayer(playerId, playerName);
            dbPlayer.CurrentCardSkin = skinName;

            await unitOfWork.SaveChangesAsync();

            CardSkin cardSkin = PurchasableItems.Skins.FirstOrDefault(s => s.Name == skinName);
            return cardSkin;
        }

        public async Task<CardSkin> BuySkin(ulong playerId, string skinName)
        {
            DbPlayer dbPlayer = await unitOfWork.PlayerRepository.FindAsync(playerId);
            CardSkin cardSkin = null;
            if (dbPlayer != null)
            {
                cardSkin = PurchasableItems.Skins.FirstOrDefault(s => s.Name == skinName);
                if(cardSkin != null)
                {
                    ulong weedCoinsCost = cardSkin.Cost;
                    dbPlayer.WeedCoins -= weedCoinsCost;
                    PlayerSkin playerSkin = new PlayerSkin
                    {
                        PlayerId = playerId,
                        SkinName = skinName,
                    };
                    unitOfWork.PlayerSkinRepository.Add(playerSkin);
                    await unitOfWork.SaveChangesAsync();
                }
            }
            return cardSkin;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        #endregion
    }
}
