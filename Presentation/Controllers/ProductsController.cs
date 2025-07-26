using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] // GET: /api/products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync();
            
            return Ok(Products);
        }

        [HttpGet("{id}")] // GET /api/products/{productId}
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);

            return Product is null ? NotFound("Product Not Found") : Ok(Product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost] // POST /api/products
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto Pdto)
        {
            var ProductCreated = await _serviceManager.ProductService.CreateProductAsync(Pdto);
        
            return Ok(ProductCreated);
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")] // PUT: /api/products/{productId}
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var Updated = await _serviceManager.ProductService.UpdateProductAsync(id, productDto);

            return Updated ? NoContent() : NotFound("Product not found.");
        }
    }
}
