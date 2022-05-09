using System;

namespace Application.Resources.Orders.Get
{
    public class StatusEntryForOrderResource
    {
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }
    }
}