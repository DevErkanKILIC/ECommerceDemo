using ECommerce.Application.Repositories.Product;
using ECommerce.Application.RequestParameters;
using ECommerce.Application.ViewModels.Products;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            });

            return Ok(new
            {
                totalCount,
                products
            });

        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] VMGetProductById product)
        {
            return Ok(await _productReadRepository.GetByIdAsync(product.Id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VMCreateProduct product)
        {
            if (ModelState.IsValid)
            {

            }
            var operation = await _productWriteRepository.AddAsync(new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VMUpdateProduct product)
        {
            Product orginProduct = await _productReadRepository.GetByIdAsync(product.Id);
            orginProduct.Name = product.Name;
            orginProduct.Stock = product.Stock;
            orginProduct.Price = product.Price;
            var operation = _productWriteRepository.Update(orginProduct);
            await _productWriteRepository.SaveAsync();
            return Ok(operation);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(VMDeleteProduct product)
        {
            var operation = await _productWriteRepository.RemoveAsync(product.Id);
            await _productWriteRepository.SaveAsync();
            return Ok(operation);
        }
    }
}
