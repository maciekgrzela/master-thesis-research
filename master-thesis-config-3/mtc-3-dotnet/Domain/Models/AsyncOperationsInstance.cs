using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class AsyncOperationsInstance
    {
        public int EntitiesAmount { get; set; }
        public long ExecutionTime { get; set; }
        public bool IsError { get; set; }
    }
}