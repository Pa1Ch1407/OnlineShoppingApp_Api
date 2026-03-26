namespace OnlineShoppingApp_Api.Models.DTO
{
    public class Customer
    {
        public string Name { get; set; }

        public int PhoneNumber { get; set; }

        public string EmailId { get; set; }

        public bool? IsActive { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public int PostalCode { get; set; }
    }
}
