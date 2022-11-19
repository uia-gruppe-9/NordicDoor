using System.ComponentModel.DataAnnotations;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Client.Models {

    public class GetTeamStatistics {

        [Required]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }




    }
}