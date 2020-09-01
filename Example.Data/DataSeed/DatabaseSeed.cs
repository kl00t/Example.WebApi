using System.Collections.Generic;
using Example.Data.Models;

namespace Example.Data.DataSeed
{
    public class DatabaseSeed
    {
        private readonly DatabaseContext _context;

        public DatabaseSeed(DatabaseContext context)
        {
            _context = context;
        }

        public void SeedDatabase()
        {
            var products = AddProducts();
            var customers = AddCustomers();
            var orders = AddOrders();

            // TODO: Link Customers / Products / Orders
        }

        private List<Order> AddOrders()
        {
            return new List<Order>();
        }

        private List<Customer> AddCustomers()
        {
            return new List<Customer>();
        }

        private List<Product> AddProducts()
        {
            return new List<Product>();
        }
    }
}