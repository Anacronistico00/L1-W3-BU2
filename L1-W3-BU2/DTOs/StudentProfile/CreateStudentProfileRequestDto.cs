using System.ComponentModel.DataAnnotations;

namespace L1_W3_BU2.DTOs.StudentProfile
{
    public class CreateStudentProfileRequestDto
    {

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
    }
}
