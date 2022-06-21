using UniversityAPIBackend.DataAccess;
using UniversityAPIBackend.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace UniversityAPIBackend.Services
{
    public class CategoriesService: BaseQueryService, ICategoriesService
    {
        public CategoriesService(UniversityDBContext dbContext) : base(dbContext)
        {
        }

        public bool CheckContext()
        {
            if(_dbContext.Category == null)
            {
                return false;
            }
            return true;
        }


        public async Task<IEnumerable<Category>?> SearchCategories(String? name)
        {
            if (CheckContext())
            {
                var query = GetAllCategories();
                if(name != null)
                {
                    query = FilterCategoriesByName(query, name);
                }
                return await query.ToListAsync();
                
            }
            return null;
        }

        // methods that return IQueryable

        private IQueryable<Category> GetAllCategories()
        {
            if (CheckContext())
            {
                return _dbContext.Category;
            }
            return new List<Category>().AsQueryable();
        }

        private IQueryable<Category> FilterCategoriesByName(IQueryable<Category> query, string name)
        {
            return query.Where((cat) => cat.Name.Contains(name));
        }


        
    }
}
