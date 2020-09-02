using System;

namespace Example.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual long CustomerId { get; set; }
        public virtual long ProductId { get; set; }
    }
}