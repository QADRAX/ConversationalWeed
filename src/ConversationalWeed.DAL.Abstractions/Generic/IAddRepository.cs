namespace ConversationalWeed.DAL.Abstractions.Generic
{
    public interface IAddRepository<T> where T : class
    {
        void Add(T entity);
    }
}
