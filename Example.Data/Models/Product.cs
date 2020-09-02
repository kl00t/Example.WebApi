using System;
using System.Collections.Generic;

namespace Example.Data.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public DateTime Created { get; set; }
    }
}
