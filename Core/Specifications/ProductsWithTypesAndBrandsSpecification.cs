using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            IncludeTypeAndBrand();
        }

        public ProductsWithTypesAndBrandsSpecification(int id) 
            : base(product => product.Id == id)
        {
            IncludeTypeAndBrand();
        }

        private void IncludeTypeAndBrand()
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(Product => Product.ProductType);
        }
    }
}