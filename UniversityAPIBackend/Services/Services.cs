using System.Linq;
using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;


namespace UniversityAPIBackend.Services
{
    public static class Services
    {
        
        // finds Users by email
        public static IEnumerable<User> findUserByEmail(IEnumerable<User> users, string email)
        {
           return users.Where(u => u.Email == email);
        }

        // Returns adult users older than age
        public static IEnumerable<Student> findOlderThanAgeStudents(IEnumerable<Student> students, int age)
        {
            DateTime newestAllowedDoB = DateTime.Now.Date.AddYears(-age);

            return students.Where(s => s.Dob.CompareTo(newestAllowedDoB) < 0);
        }

        // returns adult students
        public static IEnumerable<Student> findOlderThanEightTeenStudents(IEnumerable<Student> students)
        {
            return findOlderThanAgeStudents(students, 18);
        }

        // returns students enrolled in at least one course
        public static IEnumerable<Student> findAlreadyEnrolledStudents(IEnumerable<Student> students)
        {
            return students.Where(s => s.Courses.Any());
        } 

        // Returns courses by level, wich have at least one student enrolled
        public static IEnumerable<Course> hasStudentsByLevel(IEnumerable<Course> courses ,Level level)
        {
            return courses.Where(c => c.Level == level && c.Students.Any());
        }

        // Returns all courses that share the especified Level and Category
        public static IEnumerable<Course> FilterCoursesByLevelAndCategory(IEnumerable<Course> courses, Level level, Category category)
        {
            return courses.Where(c => c.Level == level && c.Categories.Contains(category));
        }

        // Return courses without enrolled students
        public static IEnumerable<Course> findCoursesWithoutStudents(IEnumerable<Course> courses)
        {
            return courses.Where(c => !c.Students.Any());
        }

    }
}
