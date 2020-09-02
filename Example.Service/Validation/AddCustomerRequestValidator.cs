using System.Collections.Generic;
using System.Linq;
using Example.Data;
using Example.Service.Services.Requests;

namespace Example.Service.Validation
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
    }
}
