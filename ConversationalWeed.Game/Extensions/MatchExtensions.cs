using ConversationalWeed.Resources;
using System.Collections.Generic;
using System.Linq;

namespace ConversationalWeed.Models
{
    public static class MatchExtensions
    {

        #region Game Actions

        /// <summary>
        /// Set initial drawing in the first round
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Match InitialDraw(this Match match)
        {
            IList<Player> players = match.Players;
            foreach (Player player in players)
            {
                match = match.PlayerDraw(player.Id, match.MaxFields);
            }
            return match;
        }

        /// <summary>
        /// makes a player drawing cards from context.Deck
        /// </summary>
        /// <param name="match"></param>
        /// <param name="playerId"></param>
        /// <param name="numberOfDraws"></param>
        /// <returns></returns>
        public static Match PlayerDraw(this Match match, ulong playerId, int numberOfDraws)
        {
            if (match.Deck.Count > 0)
            {
                Player player = match.Players.First(p => p.Id == playerId);
                for (int i = 0; i < numberOfDraws; i++)
                {
                    Card drawedCard = match.Deck.First();
                    player.Hand.Add(drawedCard);
                    match.Deck.Remove(drawedCard);
                }
            }
            return match;
        }

        /// <summary>
        /// Player's round draws a card from the deck
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Match RoundDraw(this Match match)
        {
            Player player = match.GetCurrentPlayer();
            if (player.Hand.Count < match.MaxFields)
            {
                match = match.PlayerDraw(player.Id, 1);
            }
            return match;
        }

        /// <summary>
        /// Increase the round
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Match IncreaseRound(this Match match)
        {
            int numberOfPlayers = match.Players.Count;
            if (match.CurrentTurn + 1 > numberOfPlayers)
            {
                match.CurrentTurn = 1;
            }
            else
            {
                match.CurrentTurn++;
            }
            if (match.Deck.Count > 0)
            {
                match = match.RoundDraw();
            }
            else
            {
                Player currentPlayer = match.GetCurrentPlayer();
                if (currentPlayer.Hand.Count == 0)
                {
                    match.GameOver = true;
                    foreach (Player player in match.Players)
                    {
                        foreach (Field field in player.Fields)
                        {
                            if (field.ProtectedValue != ProtectedFieldValue.Busted
                                && field.Value != FieldValue.Dandelion)
                            {
                                int fieldValue = (int)field.Value;
                                player.Points += fieldValue;
                            }
                        }
                    }
                }
            }

            match.CurrentRound++;

            return match;
        }

        /// <summary>
        /// get player who has the current turn
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Player GetCurrentPlayer(this Match match)
        {
            Player player = match.Players.First(p => p.Order == match.CurrentTurn);
            return player;
        }

        /// <summary>
        /// Get a picture of the game for responses
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static MatchResult ToMatchResult(this Match match)
        {
            MatchResult result = new MatchResult
            {
                DeckSize = match.Deck.Count,
                Players = match.Players,
                Round = match.CurrentRound,
                Turn = match.CurrentTurn,
                GameOver = match.GameOver,
                CurrentPlayer = match.GetCurrentPlayer(),
                IsCurrentPlayerBrick = match.CurrentPlayerBrick,
                RoundsLeft = match.TotalCards - match.CurrentRound,
            };
            return result;
        }

        #endregion

        #region Validations

        /// <summary>
        /// Is player's Turn
        /// </summary>
        /// <param name="context"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool IsPlayersTurn(this Match context, ulong playerId)
        {
            bool result = false;

            Player player = context.Players.First(p => p.Order == context.CurrentTurn);
            if (player.Id == playerId)
            {
                result = true;
            }

            return result;
        }

        public static IList<string> ValidateActionCard(this Match context, PlayCardRequest request)
        {
            IList<string> invalidReasons = new List<string>();
            bool isPlayerTurn = context.IsPlayersTurn(request.PlayerId);
            if (!isPlayerTurn)
            {
                invalidReasons.Add(Literals.ValidationIsNotYourTurn);
            }
            else
            {
                Player targetPlayer = context.Players.FirstOrDefault(p => p.Id == request.TargetPlayerId);
                if (targetPlayer == null)
                {
                    invalidReasons.Add(Literals.ValidationTargetPlayerNotFound);
                }
                else
                {
                    Player player = context.Players.First(p => p.Id == request.PlayerId);
                }

            }
            return invalidReasons;
        }

        #endregion
    }
}
