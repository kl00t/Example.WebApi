using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Example.Data;
using Example.Service.Services.Requests;
using Example.Service.Services.Responses;
using Example.Service.Services.Validation;

namespace Example.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IAddCustomerRequestValidator _validator;

        public CustomerService(DatabaseContext context, IMapper mapper, IAddCustomerRequestValidator validator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public GetAllCustomersResponse GetAllCustomers()
        {
            var customers = _context.Customer.ToList();

            return new GetAllCustomersResponse
            {
                Customers = _mapper.Map<List<Models.Customer>>(customers)
            };
        }

        public void AddCustomer(AddCustomerRequest request)
        {
            var validationResult = _validator.ValidateRequest(request);

            if (!validationResult.PassedValidation)
            {
                throw new ArgumentException(validationResult.Errors.First());
            }

            var newCustomer = _mapper.Map<Data.Models.Customer>(request);

            _context.Customer.Add(newCustomer);
            _context.SaveChanges();
        }

        public GetCustomerResponse GetCustomer(long customerId)
        {
            var customer = _context.Customer.Find(customerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"The customer id: {customerId} does not exist.");
            }

            return new GetCustomerResponse
            {
                Customer = _mapper.Map<Models.Customer>(customer)
            };
        }

        public void DeleteCustomer(long customerId)
        {
            var customer = _context.Customer.Find(customerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"The customer id: {customerId} does not exist.");
            }

            _context.Customer.Remove(customer);
            _context.SaveChanges();
        }
    }
}