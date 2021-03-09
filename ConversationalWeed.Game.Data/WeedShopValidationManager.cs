using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DB.Models;
using ConversationalWeed.Game.Data.Abstractions;
using ConversationalWeed.Game.Data.Constants;
using ConversationalWeed.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationalWeed.Game.Data
{
    public class WeedShopValidationManager : DataManager, IWeedShopValidationManager
    {
        public WeedShopValidationManager(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region Public methods

        public async Task<IList<string>> ValidateSetCurrentSkin(ulong playerId, string skinName)
        {
            IList<string> invalidReasons = new List<string>();

            if (skinName != PurchasableItems.DEFAULT_SKIN_NAME)
            {
                var existingShopSkin = PurchasableItems.Skins.FirstOrDefault(s => s.Name == skinName);
                if (existingShopSkin == null)
                {
                    invalidReasons.Add(Literals.ValidationNoItemInShop);
                }
                else
                {
                    IEnumerable<PlayerSkin> playerSkins = await unitOfWork.PlayerSkinRepository.FindAsync(playerId);
                    var existingPlayerSkin = playerSkins.FirstOrDefault(playerSkins => playerSkins.SkinName == skinName);
                    if (existingPlayerSkin == null)
                    {
                        invalidReasons.Add(Literals.ValidateNoItemInPlayersInventory);
                    }
                }
            }

            return invalidReasons;
        }

        public async Task<IList<string>> ValidateBuySkin(ulong playerId, string skinName)
        {
            IList<string> invalidReasons = new List<string>();

            var dbPlayer = await unitOfWork.PlayerRepository.FindAsync(playerId);
            var existingShopSkin = PurchasableItems.Skins.FirstOrDefault(s => s.Name == skinName);

            if (existingShopSkin == null)
            {
                invalidReasons.Add(Literals.ValidationNoItemInShop);
            }
            else
            {
                if (dbPlayer != null)
                {
                    if (dbPlayer.WeedCoins < existingShopSkin.Cost)
                    {
                        invalidReasons.Add(Literals.ValidationNotEnoughMoney);
                    }
                }
                else
                {
                    invalidReasons.Add(Literals.ValidationNotEnoughMoney);
                }
            }

            return invalidReasons;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        #endregion
    }
}
