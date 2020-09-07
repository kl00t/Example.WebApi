namespace Example.Service.Models
{
    public class Customer
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}