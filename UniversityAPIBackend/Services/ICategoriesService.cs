using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public interface ICategoriesService
    {

        Task<IEnumerable<Category>?> SearchCategories(String? name);

    }
}
