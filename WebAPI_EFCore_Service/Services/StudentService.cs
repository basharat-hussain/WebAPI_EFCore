using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_EFCore_Data.Context;
using WebAPI_EFCore_Data.Models;
using WebAPI_EFCore_Service.Interfaces;

namespace WebAPI_EFCore_Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly APIDBContext _context;

        public StudentService(APIDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>> GetStudents()
        {
            var students = await _context.Students.Where(s => s.Status).ToListAsync();
            return students;
        }
        public async Task<Student> GetStudentById(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.Status);
            return student;
        }
        public async Task<Student> AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
        public async Task<Student> UpdateStudent(int id, Student student)
        {
            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.Status);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Parentage = student.Parentage;
                existingStudent.Address = student.Address;
                await _context.SaveChangesAsync();
                return existingStudent;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.Status);
            if (existingStudent != null)
            {
                existingStudent.Status = false;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
