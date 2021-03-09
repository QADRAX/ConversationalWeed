using ConversationalWeed.DAL.Abstractions;
using ConversationalWeed.Game.Data.Constants;
using System.Threading.Tasks;
using DbPlayer = ConversationalWeed.DB.Models.Player;

namespace ConversationalWeed.Game.Data
{
    public abstract class DataManager
    {
        #region Fields

        protected readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructor

        public DataManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Protected Methods

        protected async Task<DbPlayer> GetOrCreatePlayer(ulong playerId, string playerName)
        {
            DbPlayer dbPlayer = await unitOfWork.PlayerRepository.FindAsync(playerId);
            if (dbPlayer == null)
            {
                dbPlayer = new DbPlayer
                {
                    Id = playerId,
                    Name = playerName,
                    CurrentCardSkin = PurchasableItems.DEFAULT_SKIN_NAME,
                };
                unitOfWork.PlayerRepository.Add(dbPlayer);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                if (dbPlayer.Name != playerName)
                {
                    dbPlayer.Name = playerName;
                    await unitOfWork.SaveChangesAsync();
                }
            }
            return dbPlayer;
        }

        #endregion
    }
}
