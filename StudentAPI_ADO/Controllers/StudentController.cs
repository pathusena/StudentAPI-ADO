using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI_ADO.Repositories.StudentRepository;

namespace StudentAPI_ADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) { 
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents() { 
            return await _studentService.GetStudents(0, -1);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var students = await _studentService.GetStudents(1, id);
            if (students != null && students.Count > 0)
            {
                return students[0];
            }
            else { 
                return NotFound("Student Not Found!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> SaveStudent(Student student)
        {
            try {
                var newStudent = await _studentService.SaveStudent(0, student);
                if (newStudent != null)
                {
                    return student;
                }
                else
                {
                    return StatusCode(500, "Failed to save the student.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");

            }
        }

        [HttpPut]
        public async Task<ActionResult<Student>> UpdateStudent(Student student)
        {
            try
            {
                var newStudent = await _studentService.SaveStudent(1, student);
                if (newStudent != null)
                {
                    return student;
                }
                else
                {
                    return StatusCode(500, "Failed to update the student.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try {
                var success = await _studentService.DeleteStudent(0, id);
                if (!success)
                {
                    return NotFound("Student not found");
                }
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
