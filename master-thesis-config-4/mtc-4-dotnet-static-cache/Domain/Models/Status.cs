using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}