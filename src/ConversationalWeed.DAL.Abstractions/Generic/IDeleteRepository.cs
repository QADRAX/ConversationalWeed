namespace ConversationalWeed.DAL.Abstractions.Generic
{
    public interface IDeleteRepository<T> where T : class
    {
        void Delete(T entity);
    }
}
