using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.Database
{
    public class SuggestionComment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Comment_ID")]


        public int Id { get; set; }

        [Required]
        [ForeignKey("Employees")]
        [Column("Employee_ID")]
        public int EmployeeId { get; set; }

        [Required]
        [ForeignKey("Suggestions")]
        [Column("Suggestion_ID")]
        public int SuggestionId { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }


    }



}