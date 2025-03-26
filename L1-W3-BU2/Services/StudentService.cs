using L1_W3_BU2.Data;
using L1_W3_BU2.DTOs.Student;
using L1_W3_BU2.DTOs.StudentProfile;
using L1_W3_BU2.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
                    Profile = new StudentProfile()
                    {
                        FirstName = student.Name,
                        LastName = student.Surname,
                        FiscalCode = student.FiscalCode,
                        BirthDate = student.BirthDate
                    }
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

        public async Task<List<Student>> GetStudentsAsync()
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

        public async Task<List<StudentDto>?> GetStudentsDtoAsync(List<Student> students)
        {
            try
            {
                List<StudentDto> StudentsDto = students.Select(s =>
                    new StudentDto()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Surname = s.Surname,
                        Email = s.Email,
                        Created = s.Created,
                        Updated = s.Updated,
                        Profile = new StudentProfileDto()
                        {
                            FirstName = s.Name,
                            LastName = s.Surname,
                            FiscalCode = s.Profile.FiscalCode,
                            BirthDate = s.Profile.BirthDate
                        }
                    }
                ).ToList();
                return StudentsDto;
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

        public async Task<StudentDto?> GetStudentDtoByIdAsync(Guid id)
        {
            try
            {
                var student = await _context.Students.Include(s => s.Profile).FirstOrDefaultAsync(s => s.Id == id);

                var studentDto = new StudentDto()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Email = student.Email,
                    Created = student.Created,
                    Updated = student.Updated,
                    Profile = new StudentProfileDto()
                    {
                        FirstName = student.Name,
                        LastName = student.Surname,
                        FiscalCode = student.Profile.FiscalCode,
                        BirthDate = student.Profile.BirthDate
                    }
                };

                return studentDto;
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

                if (existingStudent == null)
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

        public async Task<Student?> UpdateStudentDtoAsync(UpdateStudentRequestDto student)
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
                    Profile = new StudentProfile()
                    {
                        FirstName = student.Name,
                        LastName = student.Surname,
                        FiscalCode = student.FiscalCode,
                        BirthDate = student.BirthDate
                    }
                };
                return newStudent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateStudentAsync(Guid id, UpdateStudentRequestDto studentDto)
        {
            try
            {
                var existingStudent = await GetStudentByIdAsync(id);

                if (existingStudent == null)
                {
                    return false;
                }

                existingStudent.Name = studentDto.Name;
                existingStudent.Surname = studentDto.Surname;
                existingStudent.Email = studentDto.Email;
                existingStudent.Updated = DateTime.Now;

                if (existingStudent.Profile != null)
                {
                    existingStudent.Profile.FirstName = studentDto.Name;
                    existingStudent.Profile.LastName = studentDto.Surname;
                    existingStudent.Profile.FiscalCode = studentDto.FiscalCode;
                    existingStudent.Profile.BirthDate = studentDto.BirthDate;
                }

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}

