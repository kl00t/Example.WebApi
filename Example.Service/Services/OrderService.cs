using System;
using Example.Data;
using Example.Service.Services.Requests;
using Example.Service.Services.Responses;

namespace Example.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }

        public void CreateOrder(CreateOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public GetOrderResponse GetOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}