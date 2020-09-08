using AutoFixture;
using AutoMapper;
using Example.Service.IoC;
using Example.Service.Services.Requests;
using FluentAssertions;
using NUnit.Framework;

namespace Example.Tests.Services
{
    [TestFixture]
    public class CustomerMappingProfileTests
    {
        private readonly IFixture _fixture;
        private readonly IMapper _mapper;

        public CustomerMappingProfileTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerMappingProfile>()).CreateMapper();
        }

        [Test]
        public void MapperConfigurations_ShouldBeValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CustomerMappingProfile>());

            // Assert
            configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void AddCustomerRequest_Maps_To_Customer_Data_Model()
        {
            // Arrange
            var addCustomerRequest = _fixture.Create<AddCustomerRequest>();

            // Act
            var customer = _mapper.Map<Data.Models.Customer>(addCustomerRequest);

            // Assert
            customer.FirstName.Should().BeEquivalentTo(addCustomerRequest.FirstName);
            customer.LastName.Should().BeEquivalentTo(addCustomerRequest.LastName);
            customer.Email.Should().BeEquivalentTo(addCustomerRequest.Email);
        }

        [Test]
        public void Customer_Data_Model_Maps_To_Customer_Service_Model()
        {
            // Arrange
            var customer = _fixture.Create<Data.Models.Customer>();

            // Act
            var result = _mapper.Map<Service.Models.Customer>(customer);

            // Assert
            result.FirstName.Should().BeEquivalentTo(customer.FirstName);
            result.LastName.Should().BeEquivalentTo(customer.LastName);
        }
    }
}