using System;
using System.Collections.Generic;

namespace Application.Resources.Bills.Get
{
    public class BillResource
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerResource Customer { get; set; }
        public Guid OrderId { get; set; }
        public OrderResource Order { get; set; }
        public List<Guid> OrderedCourses { get; set; }
        public double NetPrice { get; set; }
        public double Tax { get; set; }
        public double GrossPrice { get; set; }
        public DateTime Created { get; set; }
    }

    public class CustomerResource 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string  Address2 { get; set; }
        public string NIP { get; set; }
    }

    public class OrderResource 
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public TableResource Table { get; set; }
        public List<StatusEntryResource> StatusEntries { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }  
    }

    public class TableResource 
    {
        public Guid Id {get; set;}

		public Guid HallId { get; set; }

		public int StartCoordinateX { get; set; }
		
		public int StartCoordinateY { get; set; }

		public int EndCoordinateX { get; set; }

		public int EndCoordinateY { get; set; }
    }

    public class StatusEntryResource 
    {
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }
        public StatusResource Status { get; set; }
        public string StatusName { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }
    }

    public class StatusResource
    {
        public Guid Id { get; set; }
        public String Name {get; set;} 
    }
}