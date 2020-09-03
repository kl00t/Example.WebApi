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
            AddCustomers();
        }

        private void AddCustomers()
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
        }
    }
}