using System;

namespace Application.Resources.Customers.Get
{
    public class CustomerResource
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string  Address2 { get; set; }
        
        public string NIP { get; set; }
    }
}