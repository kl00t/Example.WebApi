using System;
using AutoFixture;
using Example.Data;
using Example.Data.Models;
using Example.Service.Services.Requests;
using Example.Service.Services.Validation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Example.Tests.Services.Validation
{
    [TestFixture]
    public class AddCustomerRequestValidatorTests
    {
        private IFixture _fixture;
        private DatabaseContext _context;
        private AddCustomerRequestValidator _addCustomerRequestValidator;

        [SetUp]
        public void SetUp()
        {
            // Boilerplate
            _fixture = new Fixture();

            //Prevent fixture from generating from entity circular references 
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            // Mock setup
            _context = new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            // Sut instantiation
            _addCustomerRequestValidator = new AddCustomerRequestValidator(_context);
        }

        [Test]
        public void ValidateRequest_AllChecksPass_ReturnsPassedValidationResult()
        {
            //arrange
            var request = GetValidRequest();

            //act
            var res = _addCustomerRequestValidator.ValidateRequest(request);

            //assert
            res.PassedValidation.Should().BeTrue();
        }

        [TestCase("")]
        [TestCase(null)]
        public void ValidateRequest_FirstNameNullOrEmpty_ReturnsFailedValidationResult(string firstName)
        {
            //arrange
            var request = GetValidRequest();
            request.FirstName = firstName;

            //act
            var res = _addCustomerRequestValidator.ValidateRequest(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("Customer First Name must be populated");
        }

        [TestCase("")]
        [TestCase(null)]
        public void ValidateRequest_LastNameNullOrEmpty_ReturnsFailedValidationResult(string lastName)
        {
            //arrange
            var request = GetValidRequest();
            request.LastName = lastName;

            //act
            var res = _addCustomerRequestValidator.ValidateRequest(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("Customer Last Name must be populated");
        }

        [TestCase("")]
        [TestCase(null)]
        public void ValidateRequest_EmailNullOrEmpty_ReturnsFailedValidationResult(string email)
        {
            //arrange
            var request = GetValidRequest();
            request.Email = email;

            //act
            var res = _addCustomerRequestValidator.ValidateRequest(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("Email must be populated");
        }

        [TestCase("user@")]
        [TestCase("@")]
        [TestCase("user")]
        [TestCase(null)]
        [TestCase("")]
        public void ValidateRequest_InvalidEmail_ReturnsFailedValidationResult(string email)
        {
            //arrange
            var request = GetValidRequest();
            request.Email = email;

            //act
            var res = _addCustomerRequestValidator.ValidateRequest(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("Email must be a valid email address");
        }

        [TestCase("user@domain.com")]
        [TestCase("user@domain-domain.com")]
        [TestCase("user@domain.net")]
        [TestCase("user@1.net")]
        [TestCase("user@domain.co.uk")]
        [TestCase("user.name@domain.com")]
        [TestCase("user.name@domain-domain.com")]
        [TestCase("user.name@domain.net")]
        [TestCase("user.name@1.net")]
        [TestCase("user.name@domain.co.uk")]
        [TestCase("user+100@domain.com")]
        [TestCase("user+100@domain-domain.com")]
        [TestCase("user+100@domain.net")]
        [TestCase("user+100@1.net")]
        [TestCase("user+100@domain.co.uk")]
        public void ValidateRequest_ValidEmail_ReturnsPassedValidationResult(string email)
        {
            //arrange
            var request = GetValidRequest();
            request.Email = email;

            //act
            var res = _addCustomerRequestValidator.ValidateRequest(request);

            //assert
            res.PassedValidation.Should().BeTrue();
        }

        [Test]
        public void ValidateRequest_CustomerWithEmailAddressAlreadyExists_ReturnsFailedValidationResult()
        {
            //arrange
            var request = GetValidRequest();

            var existingPatient = _fixture
                .Build<Customer>()
                .With(x => x.Email, request.Email)
                .Create();

            _context.Add(existingPatient);
            _context.SaveChanges();

            //act
            var res = _addCustomerRequestValidator.ValidateRequest(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("A customer with that email address already exists");
        }

        private AddCustomerRequest GetValidRequest()
        {
            var customer = _fixture.Create<Customer>();
            _context.Customer.Add(customer);
            _context.SaveChanges();

            var request = _fixture.Build<AddCustomerRequest>()
                .With(x => x.Email, "user@domain.com")
                .Create();
            return request;
        }
    }
}