using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.API
{

    public class AddEventRequest
    {
        public int EmployeeId { get; set; }
        public int SuggestionId { get; set; }
        public string? Description { get; set; }
        public DateTime Timestamp { get; set; }
    }


}