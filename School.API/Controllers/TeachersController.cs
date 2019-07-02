using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.API.Models;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly SchoolContext _context;

        public TeachersController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacher()
        {
            return await _context.Teacher.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);

            if (teacher == null) return NotFound();

            return teacher;
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id) return BadRequest();

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            var teachers = _context.Teacher.Any(t => t.Email == teacher.Email);
            if (teachers) return BadRequest(teacher);
            _context.Teacher.Add(teacher);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TeacherExists(teacher.Id))
                    return Conflict();
                throw;
            }

            return CreatedAtAction("GetTeacher", new {id = teacher.Id}, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null) return NotFound();

            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        [HttpGet("courseDetail")]
        public async Task<ActionResult> GetCourseDetail([FromQuery] int courseId)
        {
            var courseStudents = await _context.StudentCourse.Where(sc => sc.CourseId == courseId)
                .Include(sc => sc.Student).ToListAsync();
            var course = await _context.Course.FindAsync(courseId);
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("courseStudents", courseStudents);
            dictionary.Add("course", course);

            return Ok(dictionary);
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.Id == id);
        }
    }
}