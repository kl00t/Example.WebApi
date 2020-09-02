using Example.Service.Services.Requests;

namespace Example.Service.Validation
{
    public interface IAddCustomerRequestValidator
    {
        ValidationResult ValidateRequest(AddCustomerRequest request);
    }
}