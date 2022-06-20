using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.Services
{
    public interface IChaptersService
    {
        Task<IEnumerable<Chapter>?> SearchChapters(Int32? courseId);

    }
}
