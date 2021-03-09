using ConversationalWeed.Game.Data.Constants;
using ConversationalWeed.Models;

using System.Linq;

namespace ConversationalWeed.DB.Models
{
    public static class PlayerSkinExtensions
    {
        public static CardSkin ToCardSkin(this PlayerSkin playerSkin)
        {
            string skinName = playerSkin.SkinName;
            CardSkin skin = PurchasableItems.Skins.FirstOrDefault(skin => skin.Name == skinName);
            if (skin == null)
            {
                skin = PurchasableItems.DEFAULT_SKIN;
            }
            return skin;
        }
    }
}
