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

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                return Ok(_customerService.GetAllCustomers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult AddCustomer(AddCustomerRequest request)
        {
            try
            {
                _customerService.AddCustomer(request);
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