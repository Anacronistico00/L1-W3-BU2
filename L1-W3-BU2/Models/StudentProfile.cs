using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace L1_W3_BU2.Models
{
    public class StudentProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(25)]
        public required string FiscalCode { get; set; }

        [Required]
        public required DateTime BirthDate { get; set; }
        public Guid StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}
