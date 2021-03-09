namespace ConversationalWeed.DAL.Abstractions.Generic
{
    public interface IUpdateRepository<T> where T : class
    {
        void Update(T entity);
    }
}
