using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public interface IStudentsService
    {

        Task<IEnumerable<Student>> GetStudentsByCourse(int courseId);

        Task<IEnumerable<Student>> GetAllStudentsNotEnroled();

        
    }
}
