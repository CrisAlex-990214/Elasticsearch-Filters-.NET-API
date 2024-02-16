using CleanArchitecture.Application.Interfaces;
using Nest;

namespace CleanArchitecture.Persistence
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly IElasticClient elasticClient;

        public GenericRepo(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public async Task<List<T>> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> query)
        {
            Func<SearchDescriptor<T>, ISearchRequest> selector = s => s.Query(query);

            var response = await elasticClient.SearchAsync(selector);
            return response.Documents.ToList();
        }
    }
}