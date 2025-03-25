using System.ComponentModel.DataAnnotations;

namespace L1_W3_BU2.DTOs.Student
{
    public class CreateStudentResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
