using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO_S;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<int> CreateProductAsync(ProductDto dto);
        Task<bool> UpdateProductAsync(int id, ProductDto dto); 

    }
}
