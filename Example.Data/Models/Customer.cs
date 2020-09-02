using System;
using System.Collections.Generic;

namespace Example.Data.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}