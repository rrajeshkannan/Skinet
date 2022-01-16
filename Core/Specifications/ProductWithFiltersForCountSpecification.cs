using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecificationParams productSpecificationParams)
            : base(product => 
                (String.IsNullOrEmpty(productSpecificationParams.Search) || product.Name.ToLower().Contains(productSpecificationParams.Search)) && 
                (!productSpecificationParams.BrandId.HasValue || product.ProductBrandId == productSpecificationParams.BrandId) && 
                (!productSpecificationParams.TypeId.HasValue || product.ProductTypeId == productSpecificationParams.TypeId))
        {
        }
    }
}