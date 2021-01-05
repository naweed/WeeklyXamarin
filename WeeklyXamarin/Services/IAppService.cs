using System.Collections.Generic;
using System.Threading.Tasks;
using WeeklyXamarin.Models;

namespace WeeklyXamarin.Services
{
    public interface IAppService
    {
        Task<List<Edition>> GetEditions();
        Task<Edition> GetEditionDetails(string editionId);
        IAsyncEnumerable<Article> SearchArticles(string searchTerm);
    }
}
