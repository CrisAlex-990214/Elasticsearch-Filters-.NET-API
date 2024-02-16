using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Indexes;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepo<Product> repo;

        public ProductController(IGenericRepo<Product> repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<List<Product>> Filter(ProductFilterDto? dto)
        {
            Func<QueryContainerDescriptor<Product>, QueryContainer> query = q => dto is null ? q.MatchAll() :
            q.Match(q => q.Field(f => f.Name).Query(dto.Name)) &&
            q.Terms(q => q.Field(f => f.Brand).Terms(dto.Brands)) &&
            q.Terms(q => q.Field(f => f.Colors).Terms(dto.Colors)) &&
            (!dto.PriceRange.Any() ? null :
            q.Range(r => r.Field(f => f.Price).LessThanOrEquals(dto.PriceRange[1])) &&
            q.Range(r => r.Field(f => f.Price).GreaterThanOrEquals(dto.PriceRange[0]))
            );

            return await repo.Filter(query);
        }
    }
}