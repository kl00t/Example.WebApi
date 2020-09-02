using System;
using Example.Data;
using Example.Service.Services;
using Example.Service.Services.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Example.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly DatabaseContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(DatabaseContext context, IProductService productService, ILogger<ProductController> logger)
        {
            _context = context;
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                return Ok(_productService.GetAllProducts());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductRequest request)
        {
            try
            {
                _productService.AddProduct(request);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}