using System.ComponentModel.DataAnnotations;

namespace L1_W3_BU2.DTOs.Student
{
    public class GetAllStudentsResponse
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public List<StudentDto> Students { get; set; }
    }
}
