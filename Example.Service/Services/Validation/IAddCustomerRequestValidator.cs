using Example.Service.Services.Requests;
using Example.Service.Validation;

namespace Example.Service.Services.Validation
{
    public interface IAddCustomerRequestValidator
    {
        ValidationResult ValidateRequest(AddCustomerRequest request);
    }
}