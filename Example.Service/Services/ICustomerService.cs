using Example.Service.Services.Requests;
using Example.Service.Services.Responses;

namespace Example.Service.Services
{
    public interface ICustomerService
    {
        GetAllCustomersResponse GetAllCustomers();
        void AddCustomer(AddCustomerRequest request);
        GetCustomerResponse GetCustomer(long customerId);
        void DeleteCustomer(long customerId);
    }
}