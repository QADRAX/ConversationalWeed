using ConversationalWeed.Game.Abstractions;
using ConversationalWeed.Game.Constants;
using ConversationalWeed.Game.Data.Abstractions;
using ConversationalWeed.Models;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationalWeed.Game
{
    public class ActionManager : IActionManager
    {
        #region Fields

        private readonly WeedContext _context;
        private readonly IValidationManager _validationManager;
        private readonly IServiceProvider _provider;

        #endregion

        #region Constructor

        public ActionManager(WeedContext context,
            IValidationManager validationManager,
            IServiceProvider provider)
        {
            _context = context;
            _validationManager = validationManager;
            _provider = provider;
        }

        #endregion

        #region Public methods


        public async Task<MatchResult> CreateMatch(GameType gameType, IList<PlayerRequest> requestedPlayers)
        {
            int maxFields = GetMaxFields(requestedPlayers.Count);
            Match newMatch = new Match();
            AddDeck(newMatch, gameType, requestedPlayers.Count);
            await AddPlayers(newMatch, requestedPlayers, maxFields);
            newMatch.InitialDraw();
            _context.Matches.Add(newMatch);
            ulong currentPlayerId = newMatch.GetCurrentPlayer().Id;
            newMatch.CurrentPlayerBrick = _validationManager.IsPlayerBrick(currentPlayerId);
            return newMatch.ToMatchResult();
        }

        public MatchResult GetMatchResult(ulong playerId)
        {
            Match match = _context.Matches.GetPlayerMatch(playerId);
            return match.ToMatchResult();
        }

        public async Task<MatchResult> PlayCardAsync(PlayCardRequest request)
        {
            Match match = _context.Matches.GetPlayerMatch(request.PlayerId);
            Player player = match.GetCurrentPlayer();
            Player targetPlayer = match.Players.First(p => p.Id == request.TargetPlayerId);
            Field targetField = targetPlayer.Fields.FirstOrDefault(f => f.Id == request.TagetPlayerFieldId);
            Field beneficiaryField = player.Fields.FirstOrDefault(f => f.Id == request.BeneficiaryPlayerFieldId);
            UseCard(request.CardType, player, targetPlayer, targetField, beneficiaryField);
            DiscardCard(match, player, request.CardType);

            MatchResult result = await IncreaseRound(match);
            return result;
        }

        public async Task<MatchResult> DiscardCardAsync(DiscardCardRequest request)
        {
            Match match = _context.Matches.GetPlayerMatch(request.PlayerId);
            Player player = match.Players.First(p => p.Id == request.PlayerId);
            DiscardCard(match, player, request.CardType);

            MatchResult result = await IncreaseRound(match);
            return result;
        }

        public MatchResult GameExit(GameExitRequest request)
        {
            Match match = _context.Matches.GetPlayerMatch(request.PlayerId);
            match.GameOver = true;
            MatchResult result = match.ToMatchResult();
            _context.Matches.Remove(match);
            return result;
        }

        #endregion

        #region Private Methods

        private int GetMaxFields(int playersCount)
        {
            int maxFields = GameConstants.DEFAULT_FIELDS;
            if (playersCount <= GameConstants.REDUCED_FIELDS_PLAYER_LIMIT)
            {
                maxFields++;
            }
            if (playersCount >= GameConstants.INCREASED_FIELDS_PLAYER_LIMIT)
            {
                maxFields--;
            }
            return maxFields;
        }

        private void AddDeck(Match match, GameType gameType, int numberOfPlayers)
        {
            IList<Card> deck = null;
            switch (gameType)
            {
                case GameType.Classic:
                    deck = GetClassicDeck();
                    break;
                case GameType.Competitive:
                    deck = GetCompetitiveDeck();
                    break;
            }
            while (deck.Count % numberOfPlayers != 0)
            {
                deck.AddCards(1, CardType.Weed1);
            }
            match.Deck = deck.Shuffle();
            match.TotalCards = match.Deck.Count;
            match.GameType = gameType;
            match.Discards = new List<Card>();
        }

        private IList<Card> GetCompetitiveDeck()
        {
            IList<Card> competitiveDeck = new List<Card>()
                .AddCards(8, CardType.Weed1)
                .AddCards(6, CardType.Weed2)
                .AddCards(4, CardType.Weed3)
                .AddCards(3, CardType.Weed4)
                .AddCards(1, CardType.Weed6)
                .AddCards(5, CardType.Dandileon)
                .AddCards(5, CardType.WeedKiller)
                .AddCards(2, CardType.Busted)
                .AddCards(3, CardType.Hippie)
                .AddCards(5, CardType.Stealer)
                .AddCards(2, CardType.Dog)
                .AddCards(1, CardType.Monzon)
                .AddCards(1, CardType.Potzilla);
            return competitiveDeck;
        }

        private IList<Card> GetClassicDeck()
        {
            IList<Card> originalDeck = new List<Card>()
                .AddCards(10, CardType.Weed1)
                .AddCards(10, CardType.Weed2)
                .AddCards(6, CardType.Weed3)
                .AddCards(3, CardType.Weed4)
                .AddCards(1, CardType.Weed6)
                .AddCards(5, CardType.Dandileon)
                .AddCards(5, CardType.WeedKiller)
                .AddCards(2, CardType.Busted)
                .AddCards(3, CardType.Hippie)
                .AddCards(5, CardType.Stealer)
                .AddCards(1, CardType.Potzilla);
            return originalDeck;
        }

        private async Task AddPlayers(Match match, IList<PlayerRequest> requestedPlayers, int maxFields)
        {
            IList<Player> players = new List<Player>();
            match.MaxFields = maxFields;
            requestedPlayers = requestedPlayers.Shuffle();
            int order = 1;
            foreach (PlayerRequest player in requestedPlayers)
            {
                string currentSkin = await GetPlayerCurrentSkin(player);
                players.Add(new Player
                {
                    Fields = new List<Field>().AddInitialFields(maxFields),
                    Hand = new List<Card>(),
                    Id = player.Id,
                    Name = player.Name,
                    Order = order,
                    Points = 0,
                    CurrentCardSkin = currentSkin,
                });
                order++;
            }
            match.Players = players;
            match.CurrentTurn = 1;
            match.CurrentRound = 1;
            match.GameOver = false;
        }

        private async Task<MatchResult> IncreaseRound(Match match)
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

            IList<PlayerStats> playerStats = null;
            if (match.Deck.Count > 0)
            {
                match.RoundDraw();
                match.CurrentRound++;
            }
            else
            {
                Player currentPlayer = match.GetCurrentPlayer();
                if (currentPlayer.Hand.Count == 0)
                {
                    playerStats = await SetGameOver(match);
                }
                else
                {
                    match.CurrentRound++;
                    match.CurrentPlayerBrick = _validationManager.IsPlayerBrick(match.GetCurrentPlayer().Id);
                }
            }
            MatchResult result = match.ToMatchResult();
            result.PlayerStats = playerStats;
            if (match.GameOver)
            {
                _context.Matches.Remove(match);
            }
            return result;
        }

        private async Task<IList<PlayerStats>> SetGameOver(Match match)
        {
            match.GameOver = true;
            IList<RegisterPlayerMatchRequest> registerPlayers = new List<RegisterPlayerMatchRequest>();
            foreach (Player player in match.Players)
            {
                int smokedPoints = player.Points;
                int weedPoints = 0;
                foreach (Field field in player.Fields)
                {
                    if (field.ProtectedValue != ProtectedFieldValue.Busted
                        && field.Value != FieldValue.Dandelion)
                    {
                        int fieldValue = (int)field.Value;
                        weedPoints += fieldValue;
                    }
                }
                player.Points += weedPoints;
                registerPlayers.Add(new RegisterPlayerMatchRequest
                {
                    PlayerId = player.Id,
                    Name = player.Name,
                    SmokedPoints = smokedPoints,
                    WeedPoints = weedPoints,
                });
            }
            ulong? winnerId = null;
            IList<Player> players = match.Players.OrderByDescending(p => p.Points).ToList();
            int bestPoints = players.First().Points;
            IList<Player> technicalTiePlayers = players.Where(p => p.Points == bestPoints).ToList();
            if (technicalTiePlayers.Count == 1)
            {
                winnerId = technicalTiePlayers.First().Id;
            }
            if (match.GameType == GameType.Competitive)
            {
                RegisterMatchRequest registerMatchRequest = new RegisterMatchRequest
                {
                    FinishedAtUtc = DateTime.UtcNow,
                    Players = registerPlayers,
                    WinnerId = winnerId,
                };
                IEnumerable<PlayerStats> playerStats = await RegisterMatch(registerMatchRequest);
                return playerStats.ToList();
            }
            else
            {
                return null;
            }
        }

        private async Task<string> GetPlayerCurrentSkin(PlayerRequest player)
        {
            using IServiceScope serviceScope = _provider.CreateScope();
            IServiceProvider services = serviceScope.ServiceProvider;
            IWeedShopManager shopManager = services.GetService<IWeedShopManager>();
            CardSkin skin = await shopManager.GetPlayerCurrentSkin(player.Id, player.Name);
            return skin.Name;
        }

        private async Task<IEnumerable<PlayerStats>> RegisterMatch(RegisterMatchRequest request)
        {
            IEnumerable<PlayerStats> playerStats = null;
            using (IServiceScope serviceScope = _provider.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;
                IGameDataManager playerManager = services.GetService<IGameDataManager>();

                playerStats = await playerManager.RegisterMatch(request);

                foreach (RegisterPlayerMatchRequest registeredPlayer in request.Players)
                {
                    double totalPoints = Convert.ToDouble(registeredPlayer.SmokedPoints + registeredPlayer.WeedPoints);
                    double multiplicador = 1 + request.Players.Count / 10;
                    if (registeredPlayer.PlayerId == request.WinnerId)
                    {
                        multiplicador += 1;
                    }
                    ulong totalWeedCoins = (ulong)(totalPoints * multiplicador);
                    await playerManager.AddWeedCoins(registeredPlayer.PlayerId, totalWeedCoins);
                }
            }
            return playerStats;
        }

        private void DiscardCard(Match match, Player player, CardType cardType)
        {
            Card card = player.Hand.First(c => c.Type == cardType);
            player.Hand.Remove(card);
            match.Discards.Add(card);
        }

        private void UseCard(CardType cardType, Player player, Player targetPlayer, Field targetField, Field beneficiaryField)
        {
            switch (cardType)
            {
                case CardType.Weed1:
                    targetField.Value = FieldValue.OnePlant;
                    targetField.ValueOwnerId = player.Id;
                    break;
                case CardType.Weed2:
                    targetField.Value = FieldValue.TwoPlants;
                    targetField.ValueOwnerId = player.Id;
                    break;
                case CardType.Weed3:
                    targetField.Value = FieldValue.ThreePlants;
                    targetField.ValueOwnerId = player.Id;
                    break;
                case CardType.Weed4:
                    targetField.Value = FieldValue.FourPlants;
                    targetField.ValueOwnerId = player.Id;
                    break;
                case CardType.Weed6:
                    targetField.Value = FieldValue.SixPlants;
                    targetField.ValueOwnerId = player.Id;
                    break;
                case CardType.Dandileon:
                    targetField.Value = FieldValue.Dandelion;
                    targetField.ValueOwnerId = player.Id;
                    break;
                case CardType.WeedKiller:
                    targetField.Value = FieldValue.Empty;
                    targetField.ValueOwnerId = null;
                    break;
                case CardType.Monzon:
                    MonzonKill(targetField);
                    break;
                case CardType.Hippie:
                    int fieldValue = (int)targetField.Value;
                    targetField.Value = FieldValue.Empty;
                    targetField.ValueOwnerId = null;
                    player.Points += fieldValue;
                    break;
                case CardType.Dog:
                    targetField.ProtectedValue = ProtectedFieldValue.Dog;
                    targetField.ProtectedValueOwnerId = player.Id;
                    break;
                case CardType.Busted:
                    targetField.ProtectedValue = ProtectedFieldValue.Busted;
                    targetField.ProtectedValueOwnerId = player.Id;
                    break;
                case CardType.Stealer:
                    if (targetField.ProtectedValue == ProtectedFieldValue.Dog)
                    {
                        ulong? originalDogOwner = targetField.ProtectedValueOwnerId;
                        targetField.ProtectedValue = ProtectedFieldValue.Free;
                        targetField.ProtectedValueOwnerId = null;

                        beneficiaryField.ProtectedValue = ProtectedFieldValue.Dog;
                        beneficiaryField.ProtectedValueOwnerId = originalDogOwner;
                    }
                    else
                    {
                        FieldValue originalFieldValue = targetField.Value;
                        ulong? originalValueOwner = targetField.ValueOwnerId;

                        targetField.Value = FieldValue.Empty;
                        targetField.ValueOwnerId = null;

                        beneficiaryField.Value = originalFieldValue;
                        beneficiaryField.ValueOwnerId = originalValueOwner;
                    }
                    break;
                case CardType.Potzilla:
                    foreach (Field targetPlayerField in targetPlayer.Fields)
                    {
                        PotziKill(targetPlayerField);
                    }
                    break;
                default:
                    break;
            }
        }

        private void MonzonKill(Field field)
        {
            if (field.ProtectedValue == ProtectedFieldValue.Busted
                || field.ProtectedValue == ProtectedFieldValue.Dog)
            {
                field.ProtectedValue = ProtectedFieldValue.Free;
                field.ProtectedValueOwnerId = null;
            }
            else
            {
                field.Value = FieldValue.Empty;
                field.ValueOwnerId = null;
            }
        }

        private void PotziKill(Field field)
        {
            if (field.ProtectedValue == ProtectedFieldValue.Dog)
            {
                field.ProtectedValue = ProtectedFieldValue.Free;
                field.ProtectedValueOwnerId = null;
            }
            else
            {
                if (field.ProtectedValue == ProtectedFieldValue.Busted)
                {
                    field.ProtectedValue = ProtectedFieldValue.Free;
                    field.ProtectedValueOwnerId = null;
                }
                field.Value = FieldValue.Empty;
                field.ValueOwnerId = null;
            }
        }

        #endregion
    }
}
