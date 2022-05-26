using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public interface ICategoriesService
    {

        IEnumerable<Course> GetCoursesByCategory(Category category);

    }
}
