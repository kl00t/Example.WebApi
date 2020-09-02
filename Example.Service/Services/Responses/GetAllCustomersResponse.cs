using System.Collections.Generic;
using Example.Service.Models;

namespace Example.Service.Services.Responses
{
    public class GetAllCustomersResponse
    {
        public List<Customer> Customers { get; set; }
    }
}