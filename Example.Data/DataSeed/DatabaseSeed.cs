using System;
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

            // TODO: Link multiple products to orders.
            LinkOrdersToCustomers(orders, customers);
            LinkOrdersToProducts(orders, products);
        }

        private void LinkOrdersToProducts(List<Order> orders, List<Product> products)
        {
            var count = 0;
            foreach (var order in orders)
            {
                order.ProductId = products[count++ % products.Count].Id;
            }

            _context.SaveChanges();
        }

        private void LinkOrdersToCustomers(List<Order> orders, List<Customer> customers)
        {
            var count = 0;
            foreach (var order in orders)
            {
                order.CustomerId = customers[count++ % customers.Count].Id;
            }

            _context.SaveChanges();
        }

        private List<Order> AddOrders()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    Id = Guid.Parse("683074b8-44c9-468b-9288-dfafa1e533c9"),
                    OrderDate = new DateTime(2020, 1, 11, 12, 15, 00),
                },
                new Order
                {
                    Id = Guid.Parse("57706153-7600-41fd-8a5e-dc60a584e21c"),
                    OrderDate = new DateTime(2020, 1, 11, 12, 30, 00)
                },
                new Order
                {
                    Id = Guid.Parse("b6aad474-5b5d-42b7-a274-1a94f74ff69a"),
                    OrderDate = new DateTime(2020, 1, 11, 14, 15, 00)
                },
                new Order
                {
                    Id = Guid.Parse("b67c2730-0c12-4236-9b1a-d5b1d22db247"),
                    OrderDate = new DateTime(2021, 1, 11, 12, 15, 00)
                },
                new Order
                {
                    Id = Guid.Parse("31924ff1-c64e-4e1e-977e-704abc062aa4"),
                    OrderDate = new DateTime(2021, 1, 12, 12, 15, 00)
                }
            };

            _context.Order.AddRange(orders);
            _context.SaveChanges();
            return orders;
        }

        private List<Customer> AddCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 100,
                    Gender = 1,
                    FirstName = "Bill",
                    LastName = "Bagly",
                    Email = "BToTheB@gmail.com",
                    DateOfBirth = new DateTime(1912, 1, 17),
                    Created = DateTime.UnixEpoch
                },
                new Customer
                {
                    Id = 173,
                    Gender = 1,
                    FirstName = "Philbert",
                    LastName = "McPlop",
                    Email = "ThePIsSilent@gmail.com",
                    DateOfBirth = new DateTime(1968, 4, 7),
                    Created = DateTime.UnixEpoch
                },
                new Customer
                {
                    Id = 159,
                    Gender = 1,
                    FirstName = "Stephen",
                    LastName = "Fry",
                    Email = "TheRealStephenFry@gmail.com",
                    DateOfBirth = new DateTime(1957, 8, 24),
                    Created = DateTime.UnixEpoch
                }
            };

            _context.Customer.AddRange(customers);
            _context.SaveChanges();
            return customers;
        }

        private List<Product> AddProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Product Alpha",
                    Created = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "Product Bravo",
                    Created = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    Name = "Product Charlie",
                    Created = DateTime.UtcNow
                }
            };

            _context.Product.AddRange(products);
            _context.SaveChanges();
            return products;
        }
    }
}