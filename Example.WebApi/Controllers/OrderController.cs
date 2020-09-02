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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly DatabaseContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(DatabaseContext context, IOrderService orderService, ILogger<OrderController> logger)
        {
            _context = context;
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetOrder(Guid orderId)
        {
            try
            {
                return Ok(_orderService.GetOrder(orderId));
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

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            try
            {
                _orderService.CreateOrder(request);
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