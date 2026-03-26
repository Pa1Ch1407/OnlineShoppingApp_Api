using OnlineShoppingApp_Api.Models.DTO;

namespace OnlineShoppingApp_Api.IServices
{
    public interface ICustomerService
    {
        //Get Customer Data
        Task<Customer> GetCustomersDataAsync(string mailId);
        //Save the customers
        Task<int> SaveCustomersAsync(Customer objCust);
        //Delete the customers
        Task<bool> DeleteCustomerAsync(string mailId);

    }
}
