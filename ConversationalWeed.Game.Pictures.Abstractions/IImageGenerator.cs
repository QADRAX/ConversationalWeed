using ConversationalWeed.Models;
using System.Collections.Generic;
using System.IO;

namespace ConversationalWeed.Game.Pictures.Abstractions
{
    public interface IImageGenerator
    {
        MemoryStream GenerateGameBoard(IList<Player> player);
        MemoryStream GeneratePlayerHand(Player player);
        MemoryStream GenerateCardPlayed(CardType cardType, string currentPlayerSkin);
    }
}
