using Nordic_Door.Shared.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Nordic_Door.Client.Models
{
    public class GetEmployeeStatisticsModel
    {
        [Required]
        public Employee Employee { get; set; }
    }
}
