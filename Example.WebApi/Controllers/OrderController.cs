using System;
using Example.Data;
using Example.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly DatabaseContext _context;

        public OrderController(DatabaseContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Get(Guid orderId)
        {
            return StatusCode(200);
        }
    }
}