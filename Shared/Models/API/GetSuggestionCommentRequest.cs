using Nordic_Door.Shared.Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordic_Door.Shared.Models.API
{
    public class GetSuggestionCommentRequest
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }



    }
}
