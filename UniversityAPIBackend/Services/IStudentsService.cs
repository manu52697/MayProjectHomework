using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();
        
    }
}
