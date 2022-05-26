using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public class StudentsService : BaseQueryService ,IStudentsService
    {
        public StudentsService(UniversityDBContext dbContext) : base(dbContext)
        {
        }


        public IEnumerable<Student> GetStudentsWithCourses()
        {
            return GetStudentsByEnrollment(true);
        }

        public IEnumerable<Student> GetStudentsWithNoCourses()
        {
            return GetStudentsByEnrollment(false);
        }

        // Returns students with or without any courses, conditional on `searchEnrolled`
        private IEnumerable<Student> GetStudentsByEnrollment(bool searchEnrolled)
        {

            if(_dbContext.Students != null)
            {
                if (searchEnrolled)
                {
                    return _dbContext.Students.Where(s => s.Courses.Any());
                }
                return _dbContext.Students.Where(s => !s.Courses.Any());
            }
            return new List<Student>();
        }
    }
}
