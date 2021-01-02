using System.Collections.Generic;
using System.Threading.Tasks;
using WeeklyXamarin.Models;

namespace WeeklyXamarin.Services
{
    public interface IAppService
    {
        Task<List<Edition>> GetEditions();
        Task<ArticlesList> GetEditionWithArticles(string editionId);
        Task<List<Article>> GetArticalForEdition(string editionId);
    }
}
