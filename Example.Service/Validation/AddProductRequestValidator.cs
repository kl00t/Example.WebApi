using System.Collections.Generic;
using System.Linq;
using Example.Data;
using Example.Service.Services.Requests;

namespace Example.Service.Validation
{
    public class AddProductRequestValidator : IAddProductRequestValidator
    {
        private readonly DatabaseContext _context;

        public AddProductRequestValidator(DatabaseContext context)
        {
            _context = context;
        }

        public ValidationResult ValidateRequest(AddProductRequest request)
        {
            var result = new ValidationResult(true);

            if (MissingRequiredFields(request, ref result))
                return result;

            return result;
        }

        private bool MissingRequiredFields(AddProductRequest request, ref ValidationResult result)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(request.Name))
                errors.Add("Product Name must be populated");

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