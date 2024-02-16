using Nest;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<List<T>> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> query);
    }
}
