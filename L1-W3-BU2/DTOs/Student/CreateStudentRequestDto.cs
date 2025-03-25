using System.ComponentModel.DataAnnotations;

namespace L1_W3_BU2.DTOs.Student
{
    public class CreateStudentRequestDto
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(50)]
        public required string Surname { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? Updated { get; set; }

    }
}
