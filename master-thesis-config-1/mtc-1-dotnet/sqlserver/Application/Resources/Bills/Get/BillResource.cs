using System;
using System.Collections.Generic;
using Application.Resources.Customers.Get;
using Application.Resources.Orders.Get;
using Domain.Models;

namespace Application.Resources.Bills.Get
{
    public class BillResource
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public List<Guid> OrderedCourses { get; set; }
        public double NetPrice { get; set; }
        public double Tax { get; set; }
        public double GrossPrice { get; set; }
        public DateTime Created { get; set; }
    }
}