using System.ComponentModel.DataAnnotations;
using Nordic_Door.Shared.Models.Database;
namespace Nordic_Door.Client.Models
{

    /*endrer team-tilhørighet*/
    public class ChangeTeamModel 
    {
        [Required]
        public int employeeid { get; set; }

        [Required]
        public List<string> Teams { get; set; }

    }
}
