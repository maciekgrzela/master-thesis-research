using System;

namespace Application.Resources.Hall.Get
{
    public class HallResource
    {
        public Guid Id { get; set; }

        public int RowNumber { get; set; }

        public int ColumnNumber { get; set; }
        
        public string Description { get; set; }
    }
}