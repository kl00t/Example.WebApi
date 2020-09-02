using Example.Data;
using Example.Service.Services.Requests;
using Example.Service.Services.Responses;

namespace Example.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DatabaseContext _context;

        public CustomerService(DatabaseContext context)
        {
            _context = context;
        }

        public GetAllCustomersResponse GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public void AddCustomer(AddCustomerRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}