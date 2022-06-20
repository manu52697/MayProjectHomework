using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public interface ICoursesService
    {
        bool CheckContext();
        Boolean CourseExists(int courseId);

        Task<Course?> GetCourseById(int id);

        Task<IEnumerable<Course>> GetCoursesWithNoChapters();

        Task<IEnumerable<Course>> GetCoursesWithFilterByCategoryNameAndStudentId(string? categoryName, int? studentId);

        Task<Course?> UpdateCourse(int courseId, Course course);

        Task<Course?> CreateCourse(Course course);

        Task<Boolean> DeleteCourse(int courseId);

        // Methods that return IQueryable<>
        IQueryable<Course> GetAllCourses();

        IQueryable<Course> FilterCourseById(IQueryable<Course> query, int id);
        IQueryable<Course> FilterAllCoursesWithNoChapters(IQueryable<Course> query);
        IQueryable<Course> FilterCoursesByStudentEnroled(IQueryable<Course> query ,int studentId);
        IQueryable<Course> FilterCoursesByCategory(IQueryable<Course> query, string categoryName);


    }
}
