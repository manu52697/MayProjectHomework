using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public class CoursesService : BaseQueryService, ICoursesService
    {
        public CoursesService(UniversityDBContext dbContext) : base(dbContext)
        {
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
