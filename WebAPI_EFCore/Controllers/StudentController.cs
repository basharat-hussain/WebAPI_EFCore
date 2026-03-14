using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI_EFCore.Entities;
using WebAPI_EFCore_Data.Models;
using WebAPI_EFCore_Service.Interfaces;
using WebAPI_EFCore_Service.Services;


namespace WebAPI_EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudents();
            if (students != null && students.Any())
            {
                logger.LogInformation("Retrieved {Count} active students.", students.Count());
                return Ok(students);

            }
            else
            {
                return NotFound("No active students found.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentById(id);
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
        public async Task<IActionResult> AddStudent(StudentDTO student)
        {
            if (student == null)
            {
                return BadRequest("Invalid student data.");
            }

            var newStudent = new Student
            {
                RollNo = student.RollNo,
                Name = student.Name,
                Address = student.Address,
                Parentage = student.Parentage,
            };

            await _studentService.AddStudent(newStudent);
            return Ok("Student added successfully.");
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateStudent(StudentDTO student, int id)
        {
            var newStudent = new Student
            {
                Name = student.Name,
                Address = student.Address,
                Parentage = student.Parentage,
            };
            var updatedStudent = await _studentService.UpdateStudent(id, newStudent);
            if (updatedStudent == null)
            {
                return NotFound("Student not found.");
            }
            else
            {
                return Ok("Student updated successfully.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var isDeleted = await _studentService.DeleteStudent(id);
            if (!isDeleted)
            {
                return NotFound("Student not found.");
            }
            else
            {
                return Ok("Student deleted successfully.");
            }
        }
    }
}
