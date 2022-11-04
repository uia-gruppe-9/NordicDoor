using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordic_Door.Shared.Models.API
{
    public class AddSuggestionCommentRequest
    {
        [Required]
        public int EmployeeId{ get; set; }
        [Required]
        public int SuggestionId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }




    }
}
