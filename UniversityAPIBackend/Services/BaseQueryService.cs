using UniversityAPIBackend.DataAccess;

namespace UniversityAPIBackend.Services
{
    public class BaseQueryService
    {
        protected readonly UniversityDBContext _dbContext;

        public BaseQueryService(UniversityDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
