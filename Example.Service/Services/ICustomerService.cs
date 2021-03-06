﻿using Example.Service.Services.Requests;
using Example.Service.Services.Responses;

namespace Example.Service.Services
{
    public interface ICustomerService
    {
        GetAllCustomersResponse GetAllCustomers();
        AddCustomerResponse AddCustomer(AddCustomerRequest request);
        GetCustomerResponse GetCustomer(long customerId);
        DeleteCustomerResponse DeleteCustomer(long customerId);
    }
}