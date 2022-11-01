using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Nordic_Door.Shared.Models.Database
{



    public class Picture
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Picture_ID")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Suggestions")]
        [Column("Suggestion_ID")]
        public int SuggestionId { get; set; }

        [Required]
        [ForeignKey("Employees")]
        [Column("Employee_ID")]

        public int EmployeeId { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; }
        [Required]
        public byte[] Image { get; set; }
    }

        


}