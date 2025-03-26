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
            try
            {
                List<Student> Students = await _studentService.GetStudentsAsync();

                if(Students == null)
                {
                    return BadRequest(new GetAllStudentsResponse()
                {
                        Message = "Something went wrong",
                        Students = null
                });
                }

                if(!Students.Any())
                {
                    return NotFound(new GetAllStudentsResponse()
                    {
                        Message = "No students found!",
                        Students = null
                    });
                }

                var StudentsDto = await _studentService.GetStudentsDtoAsync(Students);

                return Ok(new GetAllStudentsResponse()
                {
                    Message = "Product correctly get",
                    Students = StudentsDto 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentRequestDto studentDto)
        {
            // Passa l'ID insieme al DTO per aggiornare lo studente
            var result = await _studentService.UpdateStudentAsync(id, studentDto);

            return result ? Ok(new { Message = "Student correctly updated!" })
                          : BadRequest(new { Message = "Something went wrong!" });
        }
    }
}
