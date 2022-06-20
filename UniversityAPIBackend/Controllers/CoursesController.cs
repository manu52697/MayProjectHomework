using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;
using UniversityAPIBackend.Services;

namespace UniversityAPIBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ICoursesService _service;

        public CoursesController(UniversityDBContext context, ICoursesService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/courses?category={String}&studentId={Int32}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses(String? category = null, Int32? studentId = null)
        {
            var courses = await _service.GetCoursesWithFilterByCategoryNameAndStudentId(category, studentId);

            if (courses.Any())
            {
                return Ok(courses);
            }

            return NoContent();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _service.GetCourseById(id);
            if (course == null)
            {
                NotFound();
            }
            return Ok(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedCourse = await _service.UpdateCourse(id, course);
                if(updatedCourse == null)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            if (!_service.CheckContext())
            {
                return Problem("Entity set 'UniversityDBContext.Courses'  is null.");
            }

            var createdCourse = await _service.CreateCourse(course);
            if (course == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);

            // Default code (for reference)
            /*
            if (_context.Courses == null)
          {
              return Problem("Entity set 'UniversityDBContext.Courses'  is null.");
          }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
            */
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var foundAndDeleted = await _service.DeleteCourse(id);
            if (foundAndDeleted)
            {
                return NoContent();
            }
            return NotFound();

            // Old code (for reference)
            /*
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
            */
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Custom Controllers


        // GET: api/Courses/with-missing-chapters
        [HttpGet("with-missing-chapter")]
        public async Task<ActionResult<Course>> GetCoursesWithoutChapter()
        {
            var courses = await _service.GetCoursesWithNoChapters();
            

            if(!courses.Any())
            {
                return NoContent();
            }
            return Ok(courses);
        }

    }
}
