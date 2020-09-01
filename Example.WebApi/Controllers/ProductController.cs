using Example.Data;
using Example.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly DatabaseContext _context;

        public ProductController(DatabaseContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
    }
}