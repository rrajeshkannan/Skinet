using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParams productSpecificationParams)
            : base(product => 
                (String.IsNullOrEmpty(productSpecificationParams.Search) || product.Name.ToLower().Contains(productSpecificationParams.Search)) && 
                (!productSpecificationParams.BrandId.HasValue || product.ProductBrandId == productSpecificationParams.BrandId) && 
                (!productSpecificationParams.TypeId.HasValue || product.ProductTypeId == productSpecificationParams.TypeId))
        {
            IncludeTypeAndBrand();
            AddOrderBy(product => product.Name);
            ApplyPaging(productSpecificationParams.PageSize * (productSpecificationParams.PageIndex - 1), productSpecificationParams.PageSize);

            if (!String.IsNullOrEmpty(productSpecificationParams.Sort))
            {
                switch (productSpecificationParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(Product => Product.Price);
                        break;
                    
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }


            
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