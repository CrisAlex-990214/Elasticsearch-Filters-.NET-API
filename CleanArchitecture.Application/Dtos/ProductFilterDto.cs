namespace CleanArchitecture.Application.Dtos
{
    public class ProductFilterDto
    {
        public string Name { get; set; }
        public string[] Brands { get; set; }
        public string[] Colors { get; set; }
        public double[] PriceRange { get; set; }
    }
}
