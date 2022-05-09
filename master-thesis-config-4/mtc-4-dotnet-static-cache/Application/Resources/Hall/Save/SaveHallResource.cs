using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Hall.Save
{
    public class SaveHallResource
    {
        [Required, Range(0, Int32.MaxValue)]
        public int RowNumber { get; set; }

        [Required, Range(0, Int32.MaxValue)]
        public int ColumnNumber { get; set; }
        public string Description { get; set; }
    }
}