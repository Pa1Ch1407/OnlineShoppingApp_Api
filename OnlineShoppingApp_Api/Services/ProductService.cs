using Microsoft.EntityFrameworkCore;
using OnlineShoppingApp_Api.IServices;
using OnlineShoppingApp_Api.Models;
using OnlineShoppingApp_Api.Models.DTO;

namespace OnlineShoppingApp_Api.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateCategory(string categoryName)
        {
            string message = "";
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(p => p.CategoryName == categoryName );
            if (existingCategory != null)
            {
                existingCategory.DateModified = DateTime.Now;
                message = "As we have existing data with same category name, Record Updated successfully.";
            }
            else
            {
                var categoryData = new Category
                {
                    CategoryName = categoryName,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };
                await _context.Categories.AddAsync(categoryData);
                message = "Record created successfully.";
            }
            //Save all changes
            await _context.SaveChangesAsync();
            return message;
        }
        public async Task<bool> DeleteCategory(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c=>c.CategoryName == categoryName);
            if(category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();
            return result>0;
        }
        public async Task<List<Category>> GetCategoriesList()
        {
            var result = await _context.Categories.ToListAsync();
            return result;
        }

        public async Task<List<ProductDTO>> GetProductList()
        {
            var result = await (from p in _context.Products
                                join pc in _context.ProductCategories on p.Id equals pc.ProductId
                                join c in _context.Categories on pc.CategoryId equals c.Id
                                join pw in _context.ProductWarehouses on p.Id equals pw.ProductId
                                select new ProductDTO
                                {
                                    ProductName = p.Description,
                                    CategoryName = c.CategoryName,
                                    TotalQuantity = pw.AvailableQuantity
                                }).ToListAsync();
            return result;
        }
        public async Task<bool> DeleteProduct(string productName)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Description == productName);
            if (product == null)
            {
                return false;
            }
            else
            {
                var productCategory = await _context.ProductCategories.Where(p => p.ProductId == product.Id).ToListAsync();
                var productWarehouse = await _context.ProductWarehouses.Where(p => p.ProductId == product.Id).ToListAsync();
                _context.ProductWarehouses.RemoveRange(productWarehouse);
                _context.ProductCategories.RemoveRange(productCategory);
                _context.Products.Remove(product);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
        }
        public async Task<int> Createproduct(ProductDTO productDTO)
        {
            if (productDTO == null || string.IsNullOrWhiteSpace(productDTO.ProductName)) throw new ArgumentException("Invalid Prioduct Data");
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Description.ToLower().Trim() == productDTO.ProductName.ToLower().Trim());
            //int id = await _context.Products.CountAsync();
            int productId;
            if (existingProduct != null)
            {
                existingProduct.DateModified = DateTime.Now;
                productId = existingProduct.Id;
            }
            else
            {
                var newProduct = new Product
                {
                    //Id = id + 1,
                    Description = productDTO.ProductName,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();
                productId = newProduct.Id;
            }
            var categoryId = await _context.Categories
                            .Where(c => c.CategoryName.ToLower().Trim() == productDTO.CategoryName.ToLower().Trim())
                            .Select(c => c.Id)
                            .FirstOrDefaultAsync();
            if (categoryId == 0)
                throw new Exception("Category not found");
            var existingProductCategory = await _context.ProductCategories.FirstOrDefaultAsync(pc => pc.ProductId == productId);

            if (existingProductCategory != null)
            {
                existingProductCategory.CategoryId = categoryId;
                existingProductCategory.DateModified = DateTime.Now;
            }
            else
            {
                var productCategory = new ProductCategory
                {
                    ProductId = productId,
                    CategoryId = categoryId,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };

                await _context.ProductCategories.AddAsync(productCategory);
            }
            var existingProductWarehouse = await _context.ProductWarehouses.FirstOrDefaultAsync(pw => pw.ProductId == productId);

            if (existingProductWarehouse != null)
            {
                existingProductWarehouse.TotalQuantity = productDTO.TotalQuantity;
                existingProductWarehouse.AvailableQuantity = productDTO.TotalQuantity;
                existingProductWarehouse.DateModified = DateTime.Now;
            }
            else
            {
                var productWarehouse = new ProductWarehouse
                {
                    ProductId = productId,
                    TotalQuantity = productDTO.TotalQuantity,
                    AvailableQuantity = productDTO.TotalQuantity,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };

                await _context.ProductWarehouses.AddAsync(productWarehouse);
            }
            await _context.SaveChangesAsync();
            return productId;
        }
    }
}
