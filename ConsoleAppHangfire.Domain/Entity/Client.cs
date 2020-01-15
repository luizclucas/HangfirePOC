using System;

namespace ConsoleAppHangfire.Domain.Entity
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string City { get; set; }
    }
}
