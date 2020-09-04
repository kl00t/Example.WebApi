using System.Collections.Generic;
using System.Linq;
using Example.Data;
using Example.Service.Services.Requests;
using Example.Service.Validation;

namespace Example.Service.Services.Validation
{
    public class AddCustomerRequestValidator : IAddCustomerRequestValidator
    {
        private readonly DatabaseContext _context;

        public AddCustomerRequestValidator(DatabaseContext context)
        {
            _context = context;
        }

        public ValidationResult ValidateRequest(AddCustomerRequest request)
        {
            var result = new ValidationResult(true);

            if (MissingRequiredFields(request, ref result))
                return result;

            if (ValidateEmail(request, ref result))
                return result;

            if (CustomerAlreadyInDb(request, ref result))
                return result;

            return result;
        }

        private bool MissingRequiredFields(AddCustomerRequest request, ref ValidationResult result)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(request.FirstName))
                errors.Add("Customer First Name must be populated");

            if (string.IsNullOrEmpty(request.LastName))
                errors.Add("Customer Last Name must be populated");

            if (errors.Any())
            {
                result.PassedValidation = false;
                result.Errors.AddRange(errors);
                return true;
            }

            return false;
        }

        private bool ValidateEmail(AddCustomerRequest request, ref ValidationResult result)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(request.Email))
                errors.Add("Email must be populated");

            if (!RegexValidation.IsEmailValid(request.Email))
                errors.Add("Email must be a valid email address");

            if (errors.Any())
            {
                result.PassedValidation = false;
                result.Errors.AddRange(errors);
                return true;
            }

            return false;
        }

        private bool CustomerAlreadyInDb(AddCustomerRequest request, ref ValidationResult result)
        {
            if (_context.Customer.Any(x => x.Email == request.Email))
            {
                result.PassedValidation = false;
                result.Errors.Add("A customer with that email address already exists");
                return true;
            }

            return false;
        }


    }
}
