using Example.Data;
using Example.Service.Services.Requests;
using Example.Service.Services.Responses;

namespace Example.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddProduct(AddProductRequest request)
        {
            throw new System.NotImplementedException();
        }

        public GetAllProductsResponse GetAllProducts()
        {
            throw new System.NotImplementedException();
        }
    }
}