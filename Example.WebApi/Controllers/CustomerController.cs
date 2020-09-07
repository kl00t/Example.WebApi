using System;
using System.Collections.Generic;
using System.Net;
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

        [HttpGet("{customerId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetCustomer(long customerId)
        {
            try
            {
                return Ok(_customerService.GetCustomer(customerId));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

        [HttpDelete("{customerId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult DeleteCustomer(long customerId)
        {
            try
            {
                _customerService.DeleteCustomer(customerId);
                return Ok();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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