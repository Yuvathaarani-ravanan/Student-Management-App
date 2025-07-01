using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;
using StudentManagementApp.Services;

namespace StudentManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentInfoService _studentService;

        public StudentController(StudentInfoService studentService)
        {
            _studentService = studentService;
        }

        // ✅ Create Student
        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent([FromBody] StudentInfo student)
        {
            await _studentService.CreateStudentAsync(student);
            return Ok("Student record added successfully.");
        }

        // 🗑️ Delete Student by Id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            await _studentService.DeleteStudentAsync(id);
            return Ok("Student deleted successfully.");
        }

        // 📊 Get Summary of Students by Course
        [HttpGet("summary/course")]
        public async Task<IActionResult> GetCourseSummary()
        {
            var summary = await _studentService.GetSummaryByCourseAsync();
            return Ok(summary);
        }

        // 📊 Get Total Student Count
        [HttpGet("summary/count")]
        public async Task<IActionResult> GetTotalCount()
        {
            var count = await _studentService.GetTotalCountAsync();
            return Ok(new { totalStudents = count });
        }

        // 🔍 Get All Students
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }
    }
}
