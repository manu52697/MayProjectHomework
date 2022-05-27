using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public class CoursesService : BaseQueryService, ICoursesService
    {
        public CoursesService(UniversityDBContext dbContext) : base(dbContext)
        {
        }

        public Chapter GetCourseChapter(Course course)
        {
            if(_dbContext.Courses != null)
            {
                return _dbContext.Courses.Where(c => c.Equals(course)).Select(c => c.Index).First();
            }

            return new Chapter();
        }

        public IEnumerable<Student> GetCourseStudents(Course course)
        {
            if(_dbContext.Students != null)
            {
                return _dbContext.Students.Where(s => s.Courses.Contains(course));
            }

            return new List<Student>();
        }

        public IEnumerable<Course> GetCoursesWithNoChapters()
        {
            if(_dbContext.Courses != null)
            {
                return _dbContext.Courses.Where(c => c.Index.List == string.Empty);
            }

            return new List<Course>();
        }
    }
}
