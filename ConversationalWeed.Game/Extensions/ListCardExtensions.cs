using System;
using System.Collections.Generic;

namespace ConversationalWeed.Models
{
    public static class ListCardExtensions
    {
        public static IList<Card> AddCards(this IList<Card> cards, int numberOfcards, CardType cardType)
        {
            for(int i = 0; i<numberOfcards; i++)
            {
                cards.Add(new Card
                {
                    Id = Guid.NewGuid(),
                    Type = cardType,
                });
            }
            return cards;
        }
    }
}
