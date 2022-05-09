using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class AsyncOperationsResult
    {
        public double AverageTimeFor100Entities { get; set; }
        public double PercentageErrorFor100Entities { get; set; }
        
        public double AverageTimeFor200Entities { get; set; }
        public double PercentageErrorFor200Entities { get; set; }
        
        public double AverageTimeFor500Entities { get; set; }
        public double PercentageErrorFor500Entities { get; set; }
        
        public double AverageTimeFor1000Entities { get; set; }
        public double PercentageErrorFor1000Entities { get; set; }
        
        public double AverageTimeFor2000Entities { get; set; }
        public double PercentageErrorFor2000Entities { get; set; }
        
        public double AverageTimeFor5000Entities { get; set; }
        public double PercentageErrorFor5000Entities { get; set; }
    }
}