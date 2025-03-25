using L1_W3_BU2.Data;
using L1_W3_BU2.DTOs.Student;
using L1_W3_BU2.DTOs.StudentProfile;
using L1_W3_BU2.Models;
using L1_W3_BU2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace L1_W3_BU2.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly StudentService _studentService;
        private readonly ILogger<StudentService> _logger;

        public StudentController(StudentService studentService, ApplicationDbContext context, ILogger<StudentService> logger)
        {
            _studentService = studentService;
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentRequestDto student)
        {
            var newStudent = await _studentService.createStudentAsync(student);

            if (newStudent == null)
            {
                return BadRequest(new
                {
                    message = "Failed to create a new student!!!"
                }
                );
            }

            var result = await _studentService.AddStudentAsync(newStudent);


            return result ? Ok(new CreateStudentResponseDto()
            {
                Message = "Student correctly added!",
            }) : BadRequest(new CreateStudentResponseDto()
            {
                Message = "Something went wrong!"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var studentsList = await _studentService.GetStudentsAsync();

            _logger.LogInformation($"Requesting students info: {JsonSerializer.Serialize(studentsList, new JsonSerializerOptions() { WriteIndented = true })}");

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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> getStudentById(Guid id)
        {
            var result = await _studentService.GetStudentByIdAsync(id);

            return result != null ? Ok(new
            {
                message = "Student correctly found!",
                student = result
            }) : BadRequest(new
            {
                message = "Something went wrong!"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);

            return result ? Ok(new
            {
                message = "Student correctly removed!",
            }) : BadRequest(new
            {
                message = "Something went wrong!"
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromQuery] Guid id, [FromBody] CreateStudentRequestDto student)
        {
            var studentToUpdate = await _studentService.updateStudentAsync(student);

            var result = await _studentService.UpdateStudentAsync(id, studentToUpdate);

            return result ? Ok(new UpdateStudentResponseDto()
            {
                Message = "Student correctly updated!",
            }) : BadRequest(new UpdateStudentResponseDto()
            {
                Message = "Something went wrong!"
            });
        }

        [HttpPost("StudentProfile")]
        public async Task<IActionResult> CreateStudentProfile([FromQuery] Guid id, [FromBody] CreateStudentProfileRequestDto student)
        {
            var newStudentProfile = await _studentService.CreateStudentProfileAsync(student, id);

            if (newStudentProfile == null)
            {
                return BadRequest(new
                {
                    message = "Failed to create a new student Profile!!!"
                }
                );
            }

            var result = await _studentService.AddStudentProfileAsync(newStudentProfile);


            return result ? Ok(new CreateStudentProfileResponseDto()
            {
                Message = "Student Profile correctly added!",
            }) : BadRequest(new CreateStudentProfileResponseDto()
            {
                Message = "Something went wrong!"
            });
        }
    }
}
