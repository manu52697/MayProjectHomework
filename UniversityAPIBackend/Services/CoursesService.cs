using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace UniversityAPIBackend.Services
{
    public class CoursesService : BaseQueryService, ICoursesService
    {
        public CoursesService(UniversityDBContext dbContext) : base(dbContext)
        {
        }

        // helper method
        public bool CheckContext()
        {
            if(_dbContext.Courses == null)
            {
                return false;
            }
            return true;
        }

        // Methods returning Tasks 

        public Boolean CourseExists(int courseId)
        {
            return GetAllCourses().Any((c) => c.Id == courseId);
        }

        public async Task<Course?> GetCourseById(int id)
        {
            var query = GetAllCourses();
            query = FilterCourseById(query, id);
            return await query.FirstAsync();
            
        }


        public async Task<IEnumerable<Course>> GetCoursesWithFilterByCategoryNameAndStudentId(string? categoryName, int? studentId)
        {
            var query = GetAllCourses();

            if (categoryName != null)
            {
                query = FilterCoursesByCategory(query, categoryName);
            }

            if (studentId != null)
            {
                query = FilterCoursesByStudentEnroled(query, (int)studentId);
            }

            var courses = await query.ToListAsync();

            return courses;
        }


        public async Task<IEnumerable<Course>> GetCoursesWithNoChapters()
        {
            var query = GetAllCourses();
            var courses = FilterAllCoursesWithNoChapters(query);
            return await courses.ToListAsync();
        }


        public async Task<Course?> UpdateCourse(int courseId, Course course)
        {
            _dbContext.Entry(course).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(courseId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return course;
        }

        public async Task<Course?> CreateCourse(Course course)
        {

            // TODO : If using DTO´s for course creation, id collision should't be a problem

            if (CourseExists(course.Id))
            {
                return null;
            }

            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<Boolean> DeleteCourse(int courseId)
        {
            if (CheckContext())
            {
                if (CourseExists(courseId))
                {
                    var course = await GetCourseById(courseId);
                    _dbContext.Courses.Remove(course);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        // Query Filtering Methods

        public IQueryable<Course> GetAllCourses()
        {
            if (_dbContext.Courses != null)
            {
                return _dbContext.Courses;
            }
            return new List<Course>().AsQueryable();
        }

        public IQueryable<Course> FilterAllCoursesWithNoChapters(IQueryable<Course> query)
        {
            return query.Where((c) => c.Index.List == String.Empty);
        }

        public IQueryable<Course> FilterCoursesByStudentEnroled(IQueryable<Course> query, int studentId)
        {
            return query.Where((c) => c.Students.Any((s) => s.Id == studentId));
        }

        public IQueryable<Course> FilterCoursesByCategory(IQueryable<Course> query, string categoryName)
        {
            return query.Where((c) => c.Categories.Any((cat) => cat.Name == categoryName));
        }

        public IQueryable<Course> FilterCourseById(IQueryable<Course> query, int id)
        {
            return query.Where((c) => c.Id == id);
        }
    }
}
