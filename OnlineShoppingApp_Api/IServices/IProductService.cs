using OnlineShoppingApp_Api.Models;
using OnlineShoppingApp_Api.Models.DTO;

namespace OnlineShoppingApp_Api.IServices
{
    public interface IProductService
    {
        Task<List<Category>> GetCategoriesList();
        Task<string> CreateCategory(string categoryName);
        Task<bool> DeleteCategory(string categoryName);
        Task<int> Createproduct(ProductDTO productDTO);
        Task<List<ProductDTO>> GetProductList();
        Task<bool> DeleteProduct(string  productName);
    }
}
