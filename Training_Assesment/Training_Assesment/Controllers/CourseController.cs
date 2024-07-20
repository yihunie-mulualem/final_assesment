using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training_Assesment.DBContext;
using Training_Assesment.Model;

namespace Training_Assesment.Controllers
{
    public class CourseController : Controller
    {
        private TrainingDbContext _context;
        public CourseController(TrainingDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/[controller]/GetCourses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            try
            {
                var courses = await _context.courses.ToListAsync();
                return courses; // Return the list of Courses as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetCoursesById")]
        public async Task<ActionResult<Course>> GetCoursesById(int id)
        {
            try
            {
                var course = await _context.courses.FindAsync(id);
                return course; // Return the list of districts as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("api/[controller]/InsertCourse")]
        public async Task<IActionResult> Insertbranches(Course course)
        {
            try
            {
                _context.courses.Add(course);
               await _context.SaveChangesAsync();
                return Ok(" Course Inserted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("api/[controller]/UpdateCourse")]
        public async Task<IActionResult> UpdateBranch(Course course)
        {
            try
            {
                // Validate district object
                if (course == null)
                {
                    return BadRequest("Course is null.");
                }
                var existingCourse = await _context.courses.FindAsync(course.Id);
                if (existingCourse == null)
                {
                    return NotFound($"Bracnh with ID {course.Id} not found.");
                }
                existingCourse.CourseCode = course.CourseCode;
                existingCourse.CourseName = course.CourseName;
                _context.courses.Update(existingCourse);
                await _context.SaveChangesAsync();
                return Ok("District updated successfully.");
            }
            catch (Exception ex)
            {
                // Return an error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                Course course =await  _context.courses.FindAsync(id);
                _context.courses.Remove(course);
               await _context.SaveChangesAsync();
                return Ok("Course Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
