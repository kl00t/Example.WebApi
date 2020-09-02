using Example.Service.Services.Requests;

namespace Example.Service.Validation
{
    public interface IAddProductRequestValidator
    {
        ValidationResult ValidateRequest(AddProductRequest request);
    }
}