using Example.Data;
using Example.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Example.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly DatabaseContext _context;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(DatabaseContext context, ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _context = context;
            _customerService = customerService;
            _logger = logger;
        }
    }
}