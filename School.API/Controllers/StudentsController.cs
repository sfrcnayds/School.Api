using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.API.Models;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.Include(s => s.Department).Where(s => s.Id == id).FirstAsync();

            if (student == null) return NotFound();

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id) return BadRequest();

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            var stu = _context.Student.Where(s => s.Mail == student.Mail || s.Number == student.Number).ToList();
            if (stu.Count > 0) return BadRequest(student);
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new {id = student.Id}, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null) return NotFound();

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        [HttpPost("selectableCourses")]
        public async Task<ActionResult> GetSelectableCourses([FromBody] int studentId)
        {
            var student = await _context.Student.FindAsync(studentId);
            var courses = await _context.Course.Include(c => c.Teacher).Where(c =>
                    c.DepartmentId == student.DepartmentId && c.StudentCourse.All(sc => sc.StudentId != studentId))
                .ToListAsync();

            return Ok(courses);
        }

        [HttpPost("selectCourse")]
        public async Task<ActionResult> SelectCourse([FromBody] StudentCourse selectCourse)
        {
            _context.StudentCourse.Add(selectCourse);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("getSelectedCourses")]
        public async Task<ActionResult> GetSelectedCourses([FromQuery] int studentId)
        {
            var studentCourses =await _context.StudentCourse.Where(sc => sc.StudentId == studentId).Include(sc=>sc.Course).Include(sc=>sc.Course.Teacher).ToListAsync();
            return Ok(studentCourses);
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}