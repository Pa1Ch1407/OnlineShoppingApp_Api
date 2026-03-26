using OnlineShoppingApp_Api.IServices;
using OnlineShoppingApp_Api.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApp_Api.Models.DTO;
using Microsoft.VisualBasic;

namespace OnlineShoppingApp_Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        public CustomerService(ApplicationDbContext applicationDbContext) 
        {
            _context = applicationDbContext;
        }
        public async Task<bool> DeleteCustomerAsync(string mailId)
        {
            var cust = await _context.CustomersData.FirstOrDefaultAsync(p => p.EmailId == mailId);
            if (cust == null) 
            {
                return false;
            }
            else
            {
                var address = _context.CustomerAddresses.Where(p => p.CustomerId == cust.Id);
                _context.CustomerAddresses.RemoveRange(address);
                _context.CustomersData.Remove(cust);
                return true;
            }
        }

        public async Task<Customer> GetCustomersDataAsync(string mailId)
        {
            var objCust = await (from cust in _context.CustomersData
                          join custAdr in _context.CustomerAddresses on cust.Id equals custAdr.CustomerId where cust.EmailId == mailId
                          select new Customer
                          {
                              Name = cust.Name,
                              PhoneNumber = cust.PhoneNumber,
                              EmailId = cust.EmailId,
                              Address = custAdr.Address,
                              City = custAdr.City,
                              State = custAdr.State,
                              Country = custAdr.Country,
                              PostalCode = custAdr.PostalCode
                          }).FirstOrDefaultAsync();
           return objCust;
                          
        }
        public async Task<int> SaveCustomersAsync(Customer objCust)
        {
            // Find existing customer
            var existingCustomer = await _context.CustomersData
                .FirstOrDefaultAsync(p => p.Name == objCust.Name
                                      && p.EmailId == objCust.EmailId
                                      && p.PhoneNumber == objCust.PhoneNumber);

            int custId;

            if (existingCustomer != null)
            {
                custId = existingCustomer.Id;
            }
            else
            {
                var cust = new CustomersData
                {
                    Name = objCust.Name,
                    PhoneNumber = objCust.PhoneNumber,
                    EmailId = objCust.EmailId,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };

                await _context.CustomersData.AddAsync(cust);
                await _context.SaveChangesAsync(); //IMPORTANT to get Id

                custId = cust.Id; //Now ID is available
            }

            //Check address
            var existingAddress = await _context.CustomerAddresses
                .FirstOrDefaultAsync(p => p.CustomerId == custId
                                      && p.Address == objCust.Address
                                      && p.PostalCode == objCust.PostalCode);

            if (existingAddress != null)
            {
                // Update
                existingAddress.Country = objCust.Country;
                existingAddress.City = objCust.City;
                existingAddress.State = objCust.State;
                existingAddress.PostalCode = objCust.PostalCode;
                existingAddress.DateModified = DateTime.Now;
            }
            else
            {
                //Insert
                var custAddress = new CustomerAddress
                {
                    CustomerId = custId,
                    Address = objCust.Address,
                    Country = objCust.Country,
                    City = objCust.City,
                    State = objCust.State,
                    PostalCode = objCust.PostalCode,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };

                await _context.CustomerAddresses.AddAsync(custAddress);
            }

            //Save all changes
            await _context.SaveChangesAsync();

            return custId; //now valid because method is async
        }
    }
}
