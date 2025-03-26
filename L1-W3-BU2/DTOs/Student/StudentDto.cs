using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using L1_W3_BU2.DTOs.StudentProfile;

namespace L1_W3_BU2.DTOs.Student
{
    public class StudentDto
    {
        [Required]
        public Guid Id { get; set; }

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

        public StudentProfileDto Profile { get; set; }
    }
}
