using System.Collections.Generic;

namespace Example.Service.Services.Responses
{
    public class GetAllCustomersResponse
    {
        public List<Customer> Customers { get; set; }

        public class Customer
        {
            public long Id { get; set; }
        }
    }
}