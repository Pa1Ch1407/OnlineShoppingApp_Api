using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OnlineShoppingApp_Api.IServices;
using OnlineShoppingApp_Api.Models.DTO;

namespace OnlineShoppingApp_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
       private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveCustomer([FromBody] Customer objCust)
        {
            if (objCust == null)
            {
                return BadRequest("Invalid data");
            }
            var result = await _customerService.SaveCustomersAsync(objCust);
            if(result != null)
            {
                return Ok(new
                {
                    message = "Customer Created."
                });
            }
            else
            {
                return BadRequest("Please check the data.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer(string mailId)
        {
            var result = await _customerService.GetCustomersDataAsync(mailId);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Customer Data not found");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(string mailId)
        {
            var result = await _customerService.DeleteCustomerAsync(mailId);
            if(result == true)
            {
                return Ok(new {
                    message = "Customer Deleted successfully.",
                });
            }
            else
            {
                return Ok(new
                {
                    message = "Customer Not found or Deleted.",
                });
            }
        }
    }
}
