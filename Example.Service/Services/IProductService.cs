using Example.Service.Services.Requests;
using Example.Service.Services.Responses;

namespace Example.Service.Services
{
    public interface IProductService
    {
        void AddProduct(AddProductRequest request);
        GetAllProductsResponse GetAllProducts();
    }
}