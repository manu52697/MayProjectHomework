using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public class CategoriesService: BaseQueryService, ICategoriesService
    {
        public CategoriesService(UniversityDBContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Course> GetCoursesByCategory(Category category)
        {
            if(_dbContext.Courses != null) {
                return _dbContext.Courses.Where(c => c.Categories.Contains(category));
            }

            return new List<Course>();
            
        }
    }
}
