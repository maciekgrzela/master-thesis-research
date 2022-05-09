using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Customers.Save
{
    public class SaveCustomerResource
    {
        [Required]
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string  Address2 { get; set; }
        
        [Required]
        public string NIP { get; set; }
    }
}