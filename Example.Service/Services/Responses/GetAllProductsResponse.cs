using System.Collections.Generic;

namespace Example.Service.Services.Responses
{
    public class GetAllProductsResponse
    {
        public List<Product> Doctors { get; set; }

        public class Product
        {
            public long Id { get; set; }
        }
    }
}