using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Example.Client;
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
        private readonly IUserClient _userClient;

        public CustomerService(DatabaseContext context, IMapper mapper, IAddCustomerRequestValidator validator, IUserClient userClient)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _userClient = userClient ?? throw new ArgumentNullException(nameof(userClient));
        }

        public GetAllCustomersResponse GetAllCustomers()
        {
            var customers = _context.Customer.ToList();

            return new GetAllCustomersResponse
            {
                Customers = _mapper.Map<List<Models.Customer>>(customers)
            };
        }

        public AddCustomerResponse AddCustomer(AddCustomerRequest request)
        {
            // Validate request.
            var validationResult = _validator.ValidateRequest(request);
            if (!validationResult.PassedValidation)
            {
                throw new ArgumentException(validationResult.Errors.First());
            }

            // TODO: Add user to service
            //var user = _mapper.Map<Client.Models.User>(request);
            //_userClient.CreateUser(user);

            var newCustomer = _mapper.Map<Data.Models.Customer>(request);
            _context.Customer.Add(newCustomer);
            _context.SaveChanges();

            // Return the created customer.
            var customer = GetCustomer(newCustomer.Id);
            return new AddCustomerResponse
            {
                Customer = customer.Customer
            };
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

        public DeleteCustomerResponse DeleteCustomer(long customerId)
        {
            var customer = _context.Customer.Find(customerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"The customer id: {customerId} does not exist.");
            }

            _context.Customer.Remove(customer);
            _context.SaveChanges();

            return new DeleteCustomerResponse
            {
                IsDeleted = true
            };
        }
    }
}