using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.Database
{
    public class SuggestionStatus
    {
        [Key]
        [Required]
        [Column("Status")]
        public string Status { get; set; }
    }
}

