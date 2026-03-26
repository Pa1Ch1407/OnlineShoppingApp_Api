using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApp_Api.IServices;
using OnlineShoppingApp_Api.Models.DTO;

namespace OnlineShoppingApp_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> SaveCategory(string categoryname)
        {
            var result = await  _productService.CreateCategory(categoryname);
            if (result == null)
            {
                return Ok(new
                {
                    message = "Invalid Data."
                });
            }
            else
            {
                return Ok(new
                {
                    message = result.ToString()
                });
            }
        }

        [HttpGet("GetCaegory")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _productService.GetCategoriesList();
            return Ok(result);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(string categoryName)
        {
            var result = await _productService.DeleteCategory(categoryName);
            return Ok(result);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.Createproduct(productDTO);
            if (result == null)
            {
                return BadRequest(new
                {
                    message = "Please check the data"
                });
            }
            else
            {
                return Ok(new
                {
                    message = "Record Saved successfully."
                });
            }
        }
        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            var result = await _productService.GetProductList();
            if(result == null)
            {
                return Ok(new
                {
                    message = "No data found."
                });
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string productName)
        {
            var result = await _productService.DeleteProduct(productName);
            return Ok(result);
        }

    }
}
