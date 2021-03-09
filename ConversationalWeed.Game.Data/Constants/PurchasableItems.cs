using ConversationalWeed.Models;
using System.Collections.Generic;

namespace ConversationalWeed.Game.Data.Constants
{
    public class PurchasableItems
    {
        public static readonly string DEFAULT_SKIN_NAME = "default";

        public static readonly CardSkin DEFAULT_SKIN = new CardSkin
        {
            Name = DEFAULT_SKIN_NAME,
            Cost = 0,
            Tier = SkinTier.Normal,
        };

        public static readonly IList<CardSkin> Skins = new List<CardSkin>()
        {
            // Normal skins
            new CardSkin
            {
                Name = "blue",
                Cost = 25,
                Tier = SkinTier.Normal,
            },
            new CardSkin
            {
                Name = "green",
                Cost = 25,
                Tier = SkinTier.Normal,
            },
            new CardSkin
            {
                Name = "orange",
                Cost = 25,
                Tier = SkinTier.Normal,
            },
            new CardSkin
            {
                Name = "red",
                Cost = 25,
                Tier = SkinTier.Normal,
            },

            // Cool skins
            new CardSkin
            {
                Name = "purple",
                Cost = 90,
                Tier = SkinTier.Cool,
            },
            new CardSkin
            {
                Name = "yellow",
                Cost = 90,
                Tier = SkinTier.Cool,
            },
            new CardSkin
            {
                Name = "pink",
                Cost = 100,
                Tier = SkinTier.Cool,
            },

            // Epic skins
            new CardSkin
            {
                Name = "night",
                Cost = 200,
                Tier = SkinTier.Epic,
            },
            new CardSkin
            {
                Name = "smoke",
                Cost = 200,
                Tier = SkinTier.Epic,
            },
            new CardSkin
            {
                Name = "sky",
                Cost = 200,
                Tier = SkinTier.Epic,
            },

            // Legendary skins
            new CardSkin
            {
                Name = "rainbow",
                Cost = 400,
                Tier = SkinTier.Legendary,
            },
            new CardSkin
            {
                Name = "gold",
                Cost = 550,
                Tier = SkinTier.Legendary,
            },
        };
    }
}
