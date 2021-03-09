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
    public class WeedHub : IWeedHub
    {
        #region Fields

        private readonly IActionManager _actionManager;
        private readonly IValidationManager _validationManager;
        private readonly IServiceProvider _provider;

        #endregion

        #region Constructor

        public WeedHub(IActionManager actionManager,
            IValidationManager validationManager,
            IServiceProvider provider)
        {
            _actionManager = actionManager;
            _validationManager = validationManager;
            _provider = provider;
        }

        #endregion

        #region Public methods

        public async Task<ValidationResult<MatchResult>> StartMatch(StartMatchRequest request)
        {
            IList<string> invalidReasons = _validationManager.ValidateStartRequest(request);
            MatchResult matchResult = null;

            if (invalidReasons.Count == 0)
            {
                matchResult = await _actionManager.CreateMatch(request.Type, request.Players);
            }
            return new ValidationResult<MatchResult>()
            {
                InvalidReasons = invalidReasons,
                Result = matchResult,
            };
        }

        public ValidationResult<MatchResult> GetMatchInfo(MatchInfoRequest request)
        {
            IList<string> invalidReasons = _validationManager.ValidateGetInfo(request);
            MatchResult matchResult = null;
            if (invalidReasons.Count == 0)
            {
                matchResult = _actionManager.GetMatchResult(request.CurrentPlayerId);
            }
            return new ValidationResult<MatchResult>()
            {
                InvalidReasons = invalidReasons,
                Result = matchResult,
            };
        }

        public async Task<ValidationResult<MatchResult>> PlayCardAsync(PlayCardRequest request)
        {
            IList<string> invalidReasons = _validationManager.ValidatePlayCard(request);
            MatchResult matchResult = null;
            if (invalidReasons.Count == 0)
            {
                matchResult = await _actionManager.PlayCardAsync(request);
            }

            return new ValidationResult<MatchResult>()
            {
                InvalidReasons = invalidReasons,
                Result = matchResult,
            };
        }

        public async Task<ValidationResult<MatchResult>> DiscardCardAsync(DiscardCardRequest request)
        {
            IList<string> invalidReasons = _validationManager.ValidateDiscardCard(request);
            MatchResult matchResult = null;
            if (invalidReasons.Count == 0)
            {
                matchResult = await _actionManager.DiscardCardAsync(request);
            }
            return new ValidationResult<MatchResult>()
            {
                InvalidReasons = invalidReasons,
                Result = matchResult,
            };
        }

        public ValidationResult<MatchResult> GameExit(GameExitRequest request)
        {
            MatchInfoRequest matchInfoRequest = new MatchInfoRequest
            {
                CurrentPlayerId = request.PlayerId,
            };
            IList<string> invalidReasons = _validationManager.ValidateGetInfo(matchInfoRequest);
            MatchResult matchResult = null;
            if (invalidReasons.Count == 0)
            {
                matchResult = _actionManager.GameExit(request);
            }
            return new ValidationResult<MatchResult>()
            {
                InvalidReasons = invalidReasons,
                Result = matchResult,
            };
        }

        public async Task<PlayerStats> GetStats(ulong playerId, string playerName)
        {
            PlayerStats playerStats = null;

            using (IServiceScope serviceScope = _provider.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;
                IGameDataManager playerManager = services.GetService<IGameDataManager>();
                playerStats = await playerManager.GetPlayerStatsAsync(playerId, playerName);
            }
            return playerStats;
        }

        public async Task<IList<CardSkin>> GetPlayerSkins(ulong playerId)
        {
            IList<CardSkin> skins = null;
            using (IServiceScope serviceScope = _provider.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;
                IWeedShopManager weedShop = services.GetService<IWeedShopManager>();
                skins = await weedShop.GetPlayerSkins(playerId);
            }
            return skins;
        }

        public async Task<IList<CardSkin>> GetPurchasableSkins(ulong playerId)
        {
            IList<CardSkin> skins = null;
            using (IServiceScope serviceScope = _provider.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;
                IWeedShopManager weedShop = services.GetService<IWeedShopManager>();
                skins = await weedShop.GetPursachableSkins(playerId);
            }
            return skins;
        }

        public async Task<ValidationResult<CardSkin>> SetCurrentSkin(ulong playerId, string playerName, string skinName)
        {
            CardSkin selectedSkin = null;
            IList<string> invalidReasons = null;
            using (IServiceScope serviceScope = _provider.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;
                IWeedShopManager weedShopManager = services.GetService<IWeedShopManager>();
                IWeedShopValidationManager validationManager = services.GetService<IWeedShopValidationManager>();

                invalidReasons = await validationManager.ValidateSetCurrentSkin(playerId, skinName);
                if (invalidReasons.Count == 0)
                {
                    selectedSkin = await weedShopManager.SetPlayerCurrentSkin(playerId, playerName, skinName);
                }
            }
            return new ValidationResult<CardSkin>()
            {
                InvalidReasons = invalidReasons,
                Result = selectedSkin,
            };
        }

        public async Task<ValidationResult<CardSkin>> BuySkin(ulong playerId, string skinName)
        {
            CardSkin selectedSkin = null;
            IList<string> invalidReasons = null;
            using (IServiceScope serviceScope = _provider.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;
                IWeedShopManager weedShopManager = services.GetService<IWeedShopManager>();
                IWeedShopValidationManager validationManager = services.GetService<IWeedShopValidationManager>();

                invalidReasons = await validationManager.ValidateBuySkin(playerId, skinName);
                if (invalidReasons.Count == 0)
                {
                    selectedSkin = await weedShopManager.BuySkin(playerId, skinName);
                }
            }
            return new ValidationResult<CardSkin>()
            {
                InvalidReasons = invalidReasons,
                Result = selectedSkin,
            };
        }

        #endregion
    }
}
