using L1_W3_BU2.Data;
using L1_W3_BU2.DTOs.Student;
using L1_W3_BU2.DTOs.StudentProfile;
using L1_W3_BU2.Models;
using Microsoft.EntityFrameworkCore;

namespace L1_W3_BU2.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StudentService> _logger;

        public StudentService(ApplicationDbContext context, ILogger<StudentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<Student?> createStudentAsync(CreateStudentRequestDto student)
        {
            try
            {
                var newStudent = new Student()
                {
                    Name = student.Name,
                    Surname = student.Surname,
                    Email = student.Email,
                    Created = student.Created,
                    Updated = student.Updated,
                };
                return newStudent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Student?> updateStudentAsync(CreateStudentRequestDto student)
        {
            try
            {
                var newStudent = new Student()
                {
                    Name = student.Name,
                    Surname = student.Surname,
                    Email = student.Email,
                    Created = student.Created,
                    Updated = student.Updated,
                };
                return newStudent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            try
            {
                _context.Students.Add(student);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Student>?> GetStudentsAsync()
        {
            try
            {
                return await _context.Students.Include(s => s.Profile).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            try
            {
                return await _context.Students.Include(s => s.Profile).FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            try
            {
                var existingStudent = await GetStudentByIdAsync(id);

                if(existingStudent == null)
                {
                    return false;
                }

                _context.Students.Remove(existingStudent);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(Guid id, Student student)
        {
            try
            {
                var existingStudent = await GetStudentByIdAsync(id);

                if (existingStudent == null)
                {
                    return false;
                }

                existingStudent.Name = student.Name;
                existingStudent.Surname = student.Surname;
                existingStudent.Email = student.Email;
                existingStudent.Updated = DateTime.Now;
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<StudentProfile?> CreateStudentProfileAsync(CreateStudentProfileRequestDto studentProfile, Guid id)
        {
            try
            {
                var newStudentProfile = new StudentProfile()
                {
                    FirstName = studentProfile.FirstName,
                    LastName = studentProfile.LastName,
                    FiscalCode = studentProfile.FiscalCode,
                    BirthDate = studentProfile.BirthDate,
                    StudentId = id,
                };

                return newStudentProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> AddStudentProfileAsync(StudentProfile studentProfile)
        {
            try
            {
                _context.StudentsProfiles.Add(studentProfile);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
