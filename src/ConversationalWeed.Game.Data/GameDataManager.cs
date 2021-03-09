using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.DB.Models;
using ConversationalWeed.Game.Data.Abstractions;
using ConversationalWeed.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbMatch = ConversationalWeed.DB.Models.Match;
using DbPlayer = ConversationalWeed.DB.Models.Player;

namespace ConversationalWeed.Game.Data
{
    public class GameDataManager : DataManager, IGameDataManager
    {
        #region Constructor

        public GameDataManager(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Public methods

        public async Task<PlayerStats> GetPlayerStatsAsync(ulong playerId, string playerName)
        {
            DbPlayer dbPlayer = await GetOrCreatePlayer(playerId, playerName);
            return dbPlayer.ToPlayerStats();
        }

        public async Task<IEnumerable<PlayerStats>> GetLeaderboard()
        {
            IEnumerable<DbPlayer> dbPlayers = await unitOfWork.PlayerRepository.GetAllAsync();
            IEnumerable<PlayerStats> leaderBoard = dbPlayers.Select(x => x.ToPlayerStats()).OrderBy(x => x.TotalWins).ToList();
            return leaderBoard;
        }

        public async Task<IEnumerable<PlayerStats>> RegisterMatch(RegisterMatchRequest request)
        {
            foreach (RegisterPlayerMatchRequest requestedPlayer in request.Players)
            {
                await GetOrCreatePlayer(requestedPlayer.PlayerId, requestedPlayer.Name);
            }

            DbMatch newMatch = new DbMatch
            {
                FinishedAtUtc = request.FinishedAtUtc,
                WinnerId = request.WinnerId,
            };
            unitOfWork.MatchRepository.Add(newMatch);
            await unitOfWork.SaveChangesAsync();

            foreach (RegisterPlayerMatchRequest requestedPlayer in request.Players)
            {
                PlayerMatch playerMatch = new PlayerMatch
                {
                    PlayerId = requestedPlayer.PlayerId,
                    MatchId = newMatch.Id,
                    SmokedPoints = requestedPlayer.SmokedPoints,
                    WeedPoints = requestedPlayer.WeedPoints,
                };
                unitOfWork.PlayerMatchRepository.Add(playerMatch);
            }
            await unitOfWork.SaveChangesAsync();

            IList<PlayerStats> result = new List<PlayerStats>();
            foreach (RegisterPlayerMatchRequest requestedPlayer in request.Players)
            {
                DbPlayer player = await GetOrCreatePlayer(requestedPlayer.PlayerId, requestedPlayer.Name);
                result.Add(player.ToPlayerStats());
            }

            return result;
        }

        public async Task AddWeedCoins(ulong playerId, ulong weedCoins)
        {
            DbPlayer dbPlayer = await unitOfWork.PlayerRepository.FindAsync(playerId);
            if (dbPlayer != null)
            {
                dbPlayer.WeedCoins += weedCoins;
                await unitOfWork.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        #endregion
    }
}
