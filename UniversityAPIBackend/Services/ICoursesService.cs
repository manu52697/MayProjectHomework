using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public interface ICoursesService
    {

        IEnumerable<Course> GetCoursesWithNoChapters();

    }
}
