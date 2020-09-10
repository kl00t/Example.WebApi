using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoMapper;
using Example.Client;
using Example.Client.Models;
using Example.Data;
using Example.Service.IoC;
using Example.Service.Models;
using Example.Service.Services;
using Example.Service.Services.Requests;
using Example.Service.Services.Responses;
using Example.Service.Services.Validation;
using Example.Service.Validation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
// ReSharper disable ObjectCreationAsStatement

namespace Example.Tests.Services
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private MockRepository _mockRepository;
        private IFixture _fixture;
        private DatabaseContext _databaseContext;
        private CustomerService _customerService;
        private IMapper _mapper;
        private Mock<IAddCustomerRequestValidator> _validator;
        private Mock<IUserClient> _userClient;

        [SetUp]
        public void SetUp()
        {
            // Boilerplate
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _fixture = new Fixture();

            //Prevent fixture from generating from entity circular references
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerMappingProfile>()).CreateMapper();

            // Mock setup
            _databaseContext = new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            _validator = _mockRepository.Create<IAddCustomerRequestValidator>();
            _userClient = _mockRepository.Create<IUserClient>();

            // Mock default
            SetupMockDefaults();

            // Sut instantiation
            _customerService = new CustomerService(
                _databaseContext,
                _mapper,
                _validator.Object,
                _userClient.Object
            );
        }

        private void SetupMockDefaults()
        {
            _validator.Setup(x => x.ValidateRequest(It.IsAny<AddCustomerRequest>()))
                .Returns(new ValidationResult(true));

            _userClient.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(""); // TODO: Setup mock success.
        }

        [Test, Order(1)]
        public void CustomerService_ThrowsArgumentNullException_With_Null_DatabaseContext()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerService(
                null, 
                Mock.Of<IMapper>(), 
                Mock.Of<IAddCustomerRequestValidator>(), 
                Mock.Of<IUserClient>()));
        }

        [Test, Order(1)]
        public void CustomerService_ThrowsArgumentNullException_With_Null_Mapper()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerService(
                _databaseContext,
                null,
                Mock.Of<IAddCustomerRequestValidator>(), 
                Mock.Of<IUserClient>()));
        }

        [Test, Order(1)]
        public void CustomerService_ThrowsArgumentNullException_With_Null_Validator()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerService(
                _databaseContext,
                Mock.Of<IMapper>(),
                null, 
                Mock.Of<IUserClient>()));
        }

        [Test]
        public void AddCustomer_ValidatesRequest()
        {
            //arrange
            var request = _fixture.Create<AddCustomerRequest>();

            //act
            _customerService.AddCustomer(request);

            //assert
            _validator.Verify(x => x.ValidateRequest(request), Times.Once);
        }

        [Test]
        public void AddCustomer_ValidatorFails_ThrowsArgumentException()
        {
            //arrange
            var failedValidationResult = new ValidationResult(false, _fixture.Create<string>());

            _validator.Setup(x => x.ValidateRequest(It.IsAny<AddCustomerRequest>())).Returns(failedValidationResult);

            //act
            var exception = Assert.Throws<ArgumentException>(() => _customerService.AddCustomer(_fixture.Create<AddCustomerRequest>()));

            //assert
            exception.Message.Should().Be(failedValidationResult.Errors.First());
        }

        [Test]
        public void AddCustomer_AddsCustomerToContextWithGeneratedId()
        {
            //arrange
            var request = _fixture.Create<AddCustomerRequest>();

            var expected = new Data.Models.Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Created = DateTime.UtcNow
            };

            //act
            _customerService.AddCustomer(request);

            //assert
            _databaseContext.Customer.Should().ContainEquivalentOf(expected, options =>
                options
                    .Excluding(patient => patient.Id)
                    .Excluding(patient => patient.Created)
            );
        }

        [Test]
        public void GetAllCustomers_NoCustomers_ReturnsEmptyList()
        {
            //arrange

            //act
            var res = _customerService.GetAllCustomers();

            //assert
            res.Customers.Should().BeEmpty();
        }

        [Test]
        public void GetAllPatients_ReturnsMappedPatientList()
        {
            //arrange
            var customer = _fixture.Create<Data.Models.Customer>();
            _databaseContext.Customer.Add(customer);
            _databaseContext.SaveChanges();

            var expected = new GetAllCustomersResponse
            {
                Customers = new List<Customer>
                {
                    new Customer
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email
                    }
                }
            };

            //act
            var res = _customerService.GetAllCustomers();

            //assert
            res.Should().BeEquivalentTo(expected);
        }

        [TearDown]
        public void TearDown()
        {
            _databaseContext.Database.EnsureDeleted();
        }
    }
}