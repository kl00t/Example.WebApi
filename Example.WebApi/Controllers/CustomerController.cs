using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
                _logger.LogError($"Error in GetCustomer: {ex.Message}");
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
            catch (Exception exception)
            {
                _logger.LogError($"Error in GetAllCustomers: {exception.Message}");
                return StatusCode(500, exception);
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
                return Ok(_customerService.DeleteCustomer(customerId));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error in DeleteCustomer: {exception.Message}");
                return StatusCode(500, exception);
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
                return Ok(_customerService.AddCustomer(request));
            }
            catch (ArgumentException exception)
            {
                _logger.LogWarning($"Argument Exception in DeleteCustomer: {exception.Message}");
                return BadRequest(exception.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteCustomer: {ex.Message}");
                return StatusCode(500, ex);
            }
        }
    }
}