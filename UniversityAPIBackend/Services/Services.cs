using System.Linq;
using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;


namespace UniversityAPIBackend.Services
{
    public class Services
    {
        private readonly UniversityDBContext _dbContext;

        public Services(UniversityDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        // finds Users by email
        public IEnumerable<User> FindUserByEmail(string email)
        {
            if (_dbContext.Users != null) { 
                return _dbContext.Users.Where(u => u.Email == email).DefaultIfEmpty();
            }

            return new List<User>();
        }

        // Returns students older than age
        public IEnumerable<Student> FindOlderThanAgeStudents(int age)
        {
            DateTime newestAllowedDoB = DateTime.Now.Date.AddYears(-age);
            // return students.Where(s => s.Dob.CompareTo(newestAllowedDoB) < 0);

            if(_dbContext.Students != null)
            {
                return _dbContext.Students.Where(s => s.Dob.CompareTo(newestAllowedDoB) < 0).DefaultIfEmpty();
            }

            return new List<Student>();
        }

        // returns adult students (older than 18)
        public IEnumerable<Student> FindOlderThanEightTeenStudents()
        {
            return FindOlderThanAgeStudents(18);
        }

        // returns students enrolled in at least one course
        public  IEnumerable<Student> FindAlreadyEnrolledStudents()
        {
            if(_dbContext.Students != null)
            {
                return _dbContext.Students.Where(s => s.Courses.Any());
            }

            return new List<Student>();
        } 

        // Returns courses by level, wich have at least one student enrolled
        public IEnumerable<Course> HasStudentsByLevel(Level level)
        {
            if(_dbContext.Courses != null)
            {
                return _dbContext.Courses.Where(c => c.Level == level && c.Students.Any());
            }
            return new List<Course>();
        }

        // Returns all courses that share the especified Level and Category
        public IEnumerable<Course> FilterCoursesByLevelAndCategory(Level level, Category category)
        {
            if(_dbContext.Courses != null)
            {
                return _dbContext.Courses.Where(c => c.Level == level && c.Categories.Contains(category));
            }
            return new List<Course>();
        }

        // Return courses without enrolled students
        public IEnumerable<Course> FindCoursesWithoutStudents()
        {
            if (_dbContext.Courses != null)
            {
                return _dbContext.Courses.Where(c => !c.Students.Any());
            }
            return new List<Course>();
            
        }

    }
}
