using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_EFCore_Data.Context;
using WebAPI_EFCore_Data.Models;

namespace WebAPI_EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly APIDBContext context;
        public StudentController(APIDBContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetStudents()
        {
            var students = context.Students.Where(s => s.Status).ToList();
            if (students != null && students.Any())
            {
                return Ok(students);

            }
            else
            {
                return NotFound("No active students found.");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = context.Students.Find(id);
            if (student != null && student.Status)
            {
                return Ok(student);
            }
            else
            {
                return NotFound("Student not found.");
            }
        }


        [HttpPost("Add")]
        public IActionResult AddStudent(Student student)
        {
            if (student == null)
            {
                return BadRequest("Invalid student data.");
            }

            context.Students.Add(student);
            context.SaveChanges();
            return Ok("Student added successfully.");
            //return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, student);
        }

        [HttpPut("Update")]
        public IActionResult UpdateStudent(Student student)
        {
            var existingStudent = context.Students.Find(student.Id);
            if (existingStudent == null)
            {
                return NotFound("Student not found.");
            }
            existingStudent.Name = student.Name;
            existingStudent.Address = student.Address;
            existingStudent.Parentage = student.Parentage;
            context.SaveChanges();
            return Ok("Student updated successfully.");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = context.Students.Find(id);
            if (student == null)
            {
                return NotFound("Student not found.");
            }
            student.Status = false; // Soft delete
            context.SaveChanges();
            return Ok("Student deleted successfully.");
        }
    }
}
