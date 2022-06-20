using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;
using UniversityAPIBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace UniversityAPIBackend.Services
{
    public class ChaptersService : BaseQueryService, IChaptersService
    {
        public ChaptersService(UniversityDBContext dbContext) : base(dbContext)
        {
        }

        private bool CheckContext()
        {
            if(_dbContext.Chapters == null)
            {
                return false;
            }
            return true;
        }


        public async Task<IEnumerable<Chapter>?> SearchChapters(Int32? courseId)
        {
            var query = GetAllChapters();
            if(courseId != null)
            {
                if(_dbContext.Courses != null && _dbContext.Courses.Any((c) => c.Id == courseId)){
                    query = FilterChaptersByCourse(query, (int)courseId);
                }
                return null;
                
            }

            return await query.ToListAsync();
        }


        // Methods that return IQueryable
        private IQueryable<Chapter> GetAllChapters()
        {
            if (CheckContext())
            {
                return _dbContext.Chapters;
            }
            return new List<Chapter>().AsQueryable();
        }

        private IQueryable<Chapter> FilterChaptersByCourse(IQueryable<Chapter> query, int courseid)
        {
            return query.Where((chapter) => chapter.CourseId == courseid);
        }

    }
}
