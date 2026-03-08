using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_EFCore_Data.Models;

namespace WebAPI_EFCore_Service.Interfaces
{
    public interface IStudentService
    {
        public Task<IEnumerable<Student>> GetStudents();
        public Task<Student> GetStudentById(int id);
        public Task<Student> AddStudent(Student student);
        public Task<Student> UpdateStudent(int id, Student student);
        public Task<bool> DeleteStudent(int id);
    }
}
