using System;
using Example.Service.Services.Requests;
using Example.Service.Services.Responses;

namespace Example.Service.Services
{
    public interface IOrderService
    {
        void CreateOrder(CreateOrderRequest request);
        GetOrderResponse GetOrder(Guid orderId);
    }
}