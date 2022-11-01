using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordic_Door.Shared.Models.Database
{

    public class SuggestionPhase 
    {
        [Key]
        [Required]
        [Column("Phase")]
        public string Phase { get; set; }



    }

}