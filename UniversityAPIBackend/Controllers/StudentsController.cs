﻿using System;
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
    public class StudentsController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly IStudentsService _studentsService;

        public StudentsController(UniversityDBContext context, IStudentsService studentsService)
        {
            _context = context;
            _studentsService = studentsService;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents([FromQuery] Int32? courseId)
        {

            IEnumerable<Student> students;

          if (_context.Students == null)
          {
              return NotFound();
          }

          if(courseId == null)
            {
                students = await _context.Students.ToListAsync();
            } else
            {
                students = await _studentsService.GetStudentsByCourse((int)courseId);
            }

            if (!students.Any())
            {
                return NoContent();
            }
            return Ok(students);  
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
          if (_context.Students == null)
          {
              return NotFound();
          }
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
          if (_context.Students == null)
          {
              return Problem("Entity set 'UniversityDBContext.Students'  is null.");
          }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // Custom endpoints
        // GET: api/Students
        [HttpGet("not-enroled")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsNotEnrolled()
        {
            var students = await _studentsService.GetAllStudentsNotEnroled();
            if (!students.Any())
            {
                return NoContent();
            }
            return Ok(students);
        }
    }
}
