using UniversityAPIBackend.DataAccess;
using Microsoft.EntityFrameworkCore;

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
