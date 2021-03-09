using ConversationalWeed.Game.Abstractions;
using ConversationalWeed.Game.Constants;
using ConversationalWeed.Models;
using ConversationalWeed.Resources;

using System.Collections.Generic;
using System.Linq;

namespace ConversationalWeed.Game
{
    public class ValidationManager : IValidationManager
    {
        #region Fields

        private readonly WeedContext _context;

        #endregion

        #region Constructor

        public ValidationManager(WeedContext context)
        {
            _context = context;
        }

        #endregion

        #region Public methods

        public IList<string> ValidateStartRequest(StartMatchRequest request)
        {
            IList<string> result = new List<string>();

            int numberOfPlayers = request.Players.Count;
            if (numberOfPlayers > GameConstants.MAX_PLAYERS_IN_MATCH
                || numberOfPlayers < GameConstants.MIN_PLAYERS_IN_MATCH)
            {
                result.Add(Literals.ValidationNumberOfPlayers);
            }

            foreach (PlayerRequest requestedPlayer in request.Players)
            {
                Match match = _context.Matches.GetPlayerMatch(requestedPlayer.Id);
                if (match != null)
                {
                    result.Add(string.Format(Literals.ValidationPlayerAlreadyInGame, requestedPlayer.Name));
                }
            }

            return result;
        }

        public IList<string> ValidateGetInfo(MatchInfoRequest request)
        {
            IList<string> invalidReasons = new List<string>();

            Match match = _context.Matches.GetPlayerMatch(request.CurrentPlayerId);
            if (match == null)
            {
                invalidReasons.Add(Literals.ValidationYouAreNotInAnyGame);
            }

            return invalidReasons;
        }

        public IList<string> ValidatePlayCard(PlayCardRequest request)
        {
            IList<string> invalidReasons = new List<string>();

            Match match = _context.Matches.GetPlayerMatch(request.PlayerId);
            if (match == null)
            {
                invalidReasons.Add(Literals.ValidationYouAreNotInAnyGame);
            }
            else
            {
                bool isPlayerTurn = match.IsPlayersTurn(request.PlayerId);
                if (!isPlayerTurn)
                {
                    invalidReasons.Add(Literals.ValidationIsNotYourTurn);
                }
                else
                {
                    Player targetPlayer = match.Players.FirstOrDefault(p => p.Id == request.TargetPlayerId);
                    if (targetPlayer == null)
                    {
                        invalidReasons.Add(Literals.ValidationTargetPlayerNotFound);
                    }
                    else
                    {
                        Player player = match.GetCurrentPlayer();
                        Card playerCard = player.Hand.FirstOrDefault(c => c.Type == request.CardType);
                        if (playerCard == null)
                        {
                            invalidReasons.Add(Literals.ValidationCardNotInHand);
                        }
                        else
                        {
                            Field targetField = targetPlayer.Fields.FirstOrDefault(f => f.Id == request.TagetPlayerFieldId);
                            Field beneficiaryField = player.Fields.FirstOrDefault(f => f.Id == request.BeneficiaryPlayerFieldId);
                            invalidReasons = ValidateCardAction(playerCard.Type, player, targetPlayer, targetField, beneficiaryField);
                        }
                    }
                }
            }
            return invalidReasons;
        }

        public IList<string> ValidateDiscardCard(DiscardCardRequest request)
        {
            IList<string> invalidReasons = new List<string>();

            Match match = _context.Matches.GetPlayerMatch(request.PlayerId);
            if (match == null)
            {
                invalidReasons.Add(Literals.ValidationYouAreNotInAnyGame);
            }
            else
            {
                bool isPlayerTurn = match.IsPlayersTurn(request.PlayerId);
                if (!isPlayerTurn)
                {
                    invalidReasons.Add(Literals.ValidationIsNotYourTurn);
                }
                else
                {
                    bool isPlayerBrick = IsPlayerBrick(request.PlayerId);
                    if (!isPlayerBrick)
                    {
                        invalidReasons.Add(Literals.ValidationYouAreNotBricked);
                    }
                }
            }
            return invalidReasons;
        }

        public bool IsPlayerBrick(ulong playerId)
        {
            Match match = _context.Matches.GetPlayerMatch(playerId);
            if (match == null)
            {
                return false;
            }
            Player player = match.Players.FirstOrDefault(p => p.Id == playerId);
            if (player == null)
            {
                return false;
            }
            bool result = true;
            bool exit = false;

            foreach (Card card in player.Hand)
            {
                foreach (Player targetPlayer in match.Players)
                {
                    foreach (Field targetField in targetPlayer.Fields)
                    {
                        foreach (Field beneficiaryField in player.Fields)
                        {
                            IList<string> invalidReasons = ValidateCardAction(card.Type, player, targetPlayer, targetField, beneficiaryField).ToList();
                            if (invalidReasons.Count == 0)
                            {
                                exit = true;
                                result = false;
                                break;
                            }
                            if (exit)
                            {
                                break;
                            }
                        }
                    }
                    if (exit)
                    {
                        break;
                    }
                }
                if (exit)
                {
                    break;
                }
            }

            return result;
        }

        #endregion

        #region Private methods

        private IList<string> ValidateCardAction(CardType cardType, Player player, Player targetPlayer, Field targetField, Field beneficiaryField)
        {
            IList<string> invalidReasons = new List<string>();

            bool fieldOwner = player.Id == targetPlayer.Id;

            switch (cardType)
            {
                case CardType.Weed1:
                    invalidReasons = ValidateWeedCard(1, fieldOwner, targetField, targetPlayer.Fields);
                    break;
                case CardType.Weed2:
                    invalidReasons = ValidateWeedCard(2, fieldOwner, targetField, targetPlayer.Fields);
                    break;
                case CardType.Weed3:
                    invalidReasons = ValidateWeedCard(3, fieldOwner, targetField, targetPlayer.Fields);
                    break;
                case CardType.Weed4:
                    invalidReasons = ValidateWeedCard(4, fieldOwner, targetField, targetPlayer.Fields);
                    break;
                case CardType.Weed6:
                    invalidReasons = ValidateWeedCard(6, fieldOwner, targetField, targetPlayer.Fields);
                    break;
                case CardType.Dandileon:
                    invalidReasons = ValidateDandelion(fieldOwner, targetField);
                    break;
                case CardType.WeedKiller:
                    invalidReasons = ValidateWeedKiller(fieldOwner, targetField);
                    break;
                case CardType.Monzon:
                    invalidReasons = ValidateMonzon(targetField);
                    break;
                case CardType.Hippie:
                    invalidReasons = ValidateHippie(fieldOwner, targetField);
                    break;
                case CardType.Dog:
                    invalidReasons = ValidateDog(targetField);
                    break;
                case CardType.Busted:
                    invalidReasons = ValidateBusted(targetField);
                    break;
                case CardType.Stealer:
                    invalidReasons = ValidateStealer(fieldOwner, targetField, beneficiaryField, player.Fields);
                    break;
                default:
                case CardType.Potzilla:
                    break;
            }

            return invalidReasons;
        }

        private IList<string> ValidateWeedCard(int weedValue, bool fieldOwner, Field targetField, IList<Field> targetPlayerFields)
        {
            IList<string> invalidReasons = new List<string>();

            if (targetField == null)
            {
                invalidReasons.Add(Literals.ValidationTargetFieldNotFound);
            }
            else
            {
                if (targetField.ProtectedValue == ProtectedFieldValue.Busted)
                {
                    invalidReasons.Add(Literals.ValidationCannotAccessBustedFields);
                }
                else
                {
                    if (targetField.ProtectedValue == ProtectedFieldValue.Dog && !fieldOwner)
                    {
                        invalidReasons.Add(Literals.ValidationCannotAccessProtectedDogFields);
                    }
                }
                if (targetField.Value == FieldValue.Dandelion)
                {
                    invalidReasons.Add(Literals.ValidationCannotPlantInDandelion);
                }
                else
                {
                    var availableEmptyTargetFields = targetPlayerFields.Where(f => f.Value == FieldValue.Empty);
                    if (!fieldOwner)
                    {
                        availableEmptyTargetFields = availableEmptyTargetFields.Where(f => f.ProtectedValue != ProtectedFieldValue.Dog);
                    }
                    if (targetField.Value != FieldValue.Empty && availableEmptyTargetFields.ToList().Count > 0)
                    {
                        invalidReasons.Add(Literals.ValidationCannotUpgradePlantIfEmptyFieldAvailable);
                    }
                    int targetWeedValue = (int)targetField.Value;
                    if (weedValue < targetWeedValue)
                    {
                        invalidReasons.Add(Literals.ValidationCannotPlantLessValueWeed);
                    }
                }
            }

            return invalidReasons;
        }

        private IList<string> ValidateDandelion(bool fieldOwner, Field targetField)
        {
            IList<string> invalidReasons = new List<string>();

            if (targetField == null)
            {
                invalidReasons.Add(Literals.ValidationTargetFieldNotFound);
            }
            else
            {
                if (targetField.ProtectedValue == ProtectedFieldValue.Busted)
                {
                    invalidReasons.Add(Literals.ValidationCannotAccessBustedFields);
                }
                else
                {
                    if (targetField.ProtectedValue == ProtectedFieldValue.Dog && !fieldOwner)
                    {
                        invalidReasons.Add(Literals.ValidationCannotAccessProtectedDogFields);
                    }
                }
                if (targetField.Value != FieldValue.Empty)
                {
                    invalidReasons.Add(Literals.ValidationYouOnlyCanPlantDandelionsInEmptyFields);
                }
            }

            return invalidReasons;
        }

        private IList<string> ValidateWeedKiller(bool fieldOwner, Field targetField)
        {
            IList<string> invalidReasons = new List<string>();

            if (targetField == null)
            {
                invalidReasons.Add(Literals.ValidationTargetFieldNotFound);
            }
            else
            {
                if (targetField.ProtectedValue == ProtectedFieldValue.Busted)
                {
                    invalidReasons.Add(Literals.ValidationCannotAccessBustedFields);
                }
                else
                {
                    if (targetField.ProtectedValue == ProtectedFieldValue.Dog && !fieldOwner)
                    {
                        invalidReasons.Add(Literals.ValidationCannotAccessProtectedDogFields);
                    }
                }
                if (targetField.Value == FieldValue.Empty)
                {
                    invalidReasons.Add(Literals.ValidationCannotKillEmptyFields);
                }
            }

            return invalidReasons;
        }

        private IList<string> ValidateMonzon(Field targetField)
        {
            IList<string> invalidReasons = new List<string>();

            if (targetField == null)
            {
                invalidReasons.Add(Literals.ValidationTargetFieldNotFound);
            }
            else
            {
                if (targetField.ProtectedValue == ProtectedFieldValue.Free && targetField.Value == FieldValue.Empty)
                {
                    invalidReasons.Add(Literals.ValidationMonzonNeedsToKillSomething);
                }
            }

            return invalidReasons;
        }

        private IList<string> ValidateHippie(bool fieldOwner, Field targetField)
        {
            IList<string> invalidReasons = new List<string>();

            if (targetField == null)
            {
                invalidReasons.Add(Literals.ValidationTargetFieldNotFound);
            }
            else
            {
                if (targetField.ProtectedValue == ProtectedFieldValue.Busted)
                {
                    invalidReasons.Add(Literals.ValidationCannotAccessBustedFields);
                }
                else
                {
                    if (targetField.ProtectedValue == ProtectedFieldValue.Dog && !fieldOwner)
                    {
                        invalidReasons.Add(Literals.ValidationCannotAccessProtectedDogFields);
                    }
                }
                if (targetField.Value == FieldValue.Empty
                    || targetField.Value == FieldValue.Dandelion)
                {
                    invalidReasons.Add(Literals.ValidationHippieNeedsToSmokeSomething);
                }
            }

            return invalidReasons;
        }

        private IList<string> ValidateDog(Field targetField)
        {
            IList<string> invalidReasons = new List<string>();

            if (targetField == null)
            {
                invalidReasons.Add(Literals.ValidationTargetFieldNotFound);
            }
            else
            {
                if (targetField.ProtectedValue == ProtectedFieldValue.Busted)
                {
                    invalidReasons.Add(Literals.ValidationCannotAccessBustedFields);
                }
                else
                {
                    if (targetField.ProtectedValue == ProtectedFieldValue.Dog)
                    {
                        invalidReasons.Add(Literals.ValidationFieldAlreadyContainsDogProtection);
                    }
                }
            }

            return invalidReasons;
        }

        private IList<string> ValidateBusted(Field targetField)
        {
            IList<string> invalidReasons = new List<string>();

            if (targetField == null)
            {
                invalidReasons.Add(Literals.ValidationTargetFieldNotFound);
            }
            else
            {
                if (targetField.ProtectedValue == ProtectedFieldValue.Busted)
                {
                    invalidReasons.Add(Literals.ValidationCannotBustedOverBusted);
                }
                else
                {
                    if (targetField.ProtectedValue == ProtectedFieldValue.Dog)
                    {
                        invalidReasons.Add(Literals.ValidationCannotAccessProtectedDogFields);
                    }
                    else
                    {
                        if (targetField.Value == FieldValue.Empty || targetField.Value == FieldValue.Dandelion)
                        {
                            invalidReasons.Add(Literals.ValidationCannotBustedNotIlegalFields);
                        }
                    }
                }
            }

            return invalidReasons;
        }

        private IList<string> ValidateStealer(bool fieldOwner, Field targetField, Field beneficiaryField, IList<Field> beneficiaryPlayerFields)
        {
            IList<string> invalidReasons = new List<string>();

            if (targetField == null)
            {
                invalidReasons.Add(Literals.ValidationTargetFieldNotFound);
            }
            else
            {
                if (fieldOwner)
                {
                    invalidReasons.Add(Literals.ValidationCannotStealYourOwnFields);
                }
                else
                {
                    if (targetField.ProtectedValue == ProtectedFieldValue.Busted)
                    {
                        invalidReasons.Add(Literals.ValidationCannotAccessBustedFields);
                    }
                    else
                    {
                        if (targetField.ProtectedValue == ProtectedFieldValue.Dog)
                        {
                            if (beneficiaryField != null)
                            {
                                invalidReasons = ValidateDog(beneficiaryField);
                            }
                            else
                            {
                                invalidReasons.Add(Literals.ValidationYouMustToIncludeBeneficiaryField);
                            }
                        }
                        else
                        {
                            if (targetField.Value != FieldValue.Empty)
                            {
                                if (beneficiaryField != null)
                                {
                                    if (targetField.Value == FieldValue.Dandelion)
                                    {
                                        invalidReasons = ValidateDandelion(false, beneficiaryField);
                                    }
                                    else
                                    {
                                        int fieldValue = (int)targetField.Value;
                                        invalidReasons = ValidateWeedCard(fieldValue, true, beneficiaryField, beneficiaryPlayerFields);
                                    }
                                }
                                else
                                {
                                    invalidReasons.Add(Literals.ValidationYouMustToIncludeBeneficiaryField);
                                }

                            }
                            else
                            {
                                invalidReasons.Add(Literals.ValidationCannotStealEmptyFields);
                            }
                        }
                    }
                }
            }

            return invalidReasons;
        }

        #endregion
    }
}
