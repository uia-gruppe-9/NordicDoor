using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.API
{

    public class AddEventRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int SuggestionId { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
    }


}