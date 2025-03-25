using System.ComponentModel.DataAnnotations;

namespace L1_W3_BU2.DTOs.StudentProfile
{
    public class CreateStudentProfileResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
