using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordic_Door.Shared.Models.Database
{

    public class Event
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Event_ID")]

        public int Id { get; set; }

        [Required]
        [ForeignKey("Employees")]
        [Column("Employee_ID")]
        public int EmployeeId { get; set; }

        [Required]
        [ForeignKey("Suggestion")]
        [Column("Suggestion_ID")]
        public int SuggestionId { get; set; }

        [Required]
        // Hvis error se på Description
        public string? Description { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }

    }



}