using ConversationalWeed.DB.Models;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace ConversationalWeed.DAL.Generic
{
    public abstract class GenericRepository<T> where T : class
    {
        #region Attributes

        private readonly DbSet<T> entities;

        #endregion

        #region Constructor

        public GenericRepository(WeedLeaderboardContext context) => entities = context.Set<T>();

        #endregion

        #region Public Methods		

        public virtual void Add(T entity) => entities.Add(entity);

        public virtual void Update(T entity) => entities.Update(entity);

        public virtual void Delete(T entity) => entities.Remove(entity);

        #endregion

        #region Protected Methods

        protected virtual IQueryable<T> GetAll() => entities.AsQueryable();

        #endregion
    }
}
