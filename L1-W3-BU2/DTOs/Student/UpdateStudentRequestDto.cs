using System.ComponentModel.DataAnnotations;

namespace L1_W3_BU2.DTOs.Student
{
    public class UpdateStudentRequestDto
    {
        [Key]
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

        public DateTime Created { get; init; }  // Non modificabile

        public DateTime Updated { get; set; } = DateTime.Now;

        [Required]
        [StringLength(25)]
        public required string FiscalCode { get; set; }

        [Required]
        public required DateTime BirthDate { get; set; }

    }
}
