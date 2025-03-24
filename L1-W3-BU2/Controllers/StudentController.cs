using L1_W3_BU2.Models;
using L1_W3_BU2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L1_W3_BU2.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            var result = await _studentService.AddStudentAsync(student);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Failed to add student!!!"
                }
                );
            }

            return Ok(new
            {
                message = "Student added successfully!"
            }
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var studentsList = await _studentService.GetStudentsAsync();
            if (studentsList == null)
            {
                return BadRequest(new
                {
                    message = "Failed to get students!!!"
                }
                );
            }

            if (!studentsList.Any())
            {
                return NoContent();
            }

            var count = studentsList.Count();

            var textMessage = count > 1 ? $"{count} Studenti trovati" : $"{count} Studente trovato";

            return Ok(new
            {
                message = textMessage,
                students = studentsList
            });
        }
    }
}
