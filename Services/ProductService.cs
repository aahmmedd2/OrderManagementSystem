using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<int> CreateProductAsync(ProductDto dto)
        {
            var Product = mapper.Map<Product>(dto);

            var ProductRepo = unitOfWork.GetRepository<Product, int>();

            await ProductRepo.AddAsync(Product);

            await unitOfWork.SaveChangesAsync();

            return Product.Id;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var ProductRepo = unitOfWork.GetRepository<Product, int>();

            var Products = await ProductRepo.GetAllAsync();

            return mapper.Map<IEnumerable<ProductDto>>(Products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var ProductRepo = unitOfWork.GetRepository<Product, int>();

            var Products = await ProductRepo.GetByIdAsync(id);

            return Products == null ? null : mapper.Map<ProductDto>(Products);
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto dto)
        {
            var ProductRepo = unitOfWork.GetRepository<Product, int>();

            var Product = await ProductRepo.GetByIdAsync(id);

            if (Product == null) return false;

            mapper.Map(dto, Product);

            ProductRepo.Update(Product);

            await unitOfWork.SaveChangesAsync();
            
            return true;
        }
    }
}
