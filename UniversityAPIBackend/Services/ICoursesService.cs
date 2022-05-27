using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public interface ICoursesService
    {

        IEnumerable<Course> GetCoursesWithNoChapters();

        Chapter GetCourseChapter(Course course);

        IEnumerable<Student> GetCourseStudents(Course course);

    }
}
