using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IEntityRepository<Product> _productsRepo;
        private readonly IEntityRepository<ProductBrand> _productBrandsRepo;
        private readonly IEntityRepository<ProductType> _productTypesRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IEntityRepository<Product> productsRepo,
            IEntityRepository<ProductBrand> productBrandsRepo,
            IEntityRepository<ProductType> productTypesRepo,
            IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productBrandsRepo = productBrandsRepo;
            _productTypesRepo = productTypesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecificationParams productSpecificationParams)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productSpecificationParams);
            var products = await _productsRepo.GetAllAsync(specification);

            var countSpecification = new ProductWithFiltersForCountSpecification(productSpecificationParams);
            var totalItems = await _productsRepo.CountAsync(countSpecification);

            var productDtos = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(
                productSpecificationParams.PageIndex, productSpecificationParams.PageSize, totalItems, productDtos));

            //return Ok(_mapper
            //    .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetAsync(specification);

            return (product != null) 
                ? Ok(_mapper.Map<ProductToReturnDto>(product)) 
                : NotFound(new ApiResponse(404));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandsRepo.GetAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypesRepo.GetAllAsync());
        }
    }
}