using L1_W3_BU2.Data;
using L1_W3_BU2.Models;
using Microsoft.EntityFrameworkCore;

namespace L1_W3_BU2.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddStudentAsync(Student student)
        {
            try
            {
                _context.Students.Add(student);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Student>?> GetStudentsAsync()
        {
            try
            {
                return await _context.Students.ToListAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
