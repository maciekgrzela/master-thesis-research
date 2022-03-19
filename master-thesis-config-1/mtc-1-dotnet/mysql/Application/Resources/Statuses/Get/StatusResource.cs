using System;
using Domain.Enums;
using Domain.Models;

namespace Application.Resources.Statuses.Get
{
    public class StatusResource
    {
        
        public Guid Id { get; set; }
        public String Name {get; set;}
    }
}