using Microsoft.EntityFrameworkCore;
using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public class StudentsService : BaseQueryService ,IStudentsService
    {
        public StudentsService(UniversityDBContext dbContext) : base(dbContext)
        {
        }

        public Boolean CheckContext()
        {
            if(_dbContext.Students == null)
            {
                return false;
            }
            return true;
        }

        // Methods that return tasks

        public async Task<IEnumerable<Student>> GetStudentsByCourse(int courseId)
        {
            var query = GetAllStudents();
            query = FilterStudentsByCourse(query, courseId);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsNotEnroled()
        {
            var query = GetAllStudents();
            query = FilterByEnrolment(query, false);
            return await query.ToListAsync();
        }


        // Methods that return IQueryable


        private IQueryable<Student> GetAllStudents()
        {
            if (CheckContext())
            {
                return _dbContext.Students;
            }
            return new List<Student>().AsQueryable();
        }

        private IQueryable<Student> FilterStudentsByCourse(IQueryable<Student> query ,int courseId)
        {
            return query.Where((s) => s.Courses.Any((c) => c.Id == courseId));
        }

        private IQueryable<Student> FilterByEnrolment(IQueryable<Student> query, bool filterEnroled)
        {
            if (filterEnroled)
            {
                return query.Where((s) => s.Courses.Any());
            }
            return query.Where((s) => !s.Courses.Any());
        }
    }
}
