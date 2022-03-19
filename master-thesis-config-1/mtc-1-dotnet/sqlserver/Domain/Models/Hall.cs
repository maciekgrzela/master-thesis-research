using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Hall
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Range(0, Int32.MaxValue)]
        public int RowNumber { get; set; }

        [Required, Range(0, Int32.MaxValue)]
        public int ColumnNumber { get; set; }

        public List<Table> Tables { get; set; }

        public string Description { get; set; }
    }
}
