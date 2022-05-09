using System;
using Application.Resources.Statuses.Get;

namespace Application.Resources.StatusEntries.Get
{
    public class StatusEntryResource
    {
        public Guid Id { get; set; }
        public StatusResource Status { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }
    }
}