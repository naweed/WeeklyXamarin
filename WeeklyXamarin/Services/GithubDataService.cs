using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonkeyCache;
using WeeklyXamarin.Framework.Services;
using WeeklyXamarin.Models;
using Xamarin.Essentials;

namespace WeeklyXamarin.Services
{
    public class GithubDataService : RestServiceBase, IAppService
    {
        public GithubDataService(string apiBaseUrl, IBarrel cacheBarrel)
            : base(apiBaseUrl, cacheBarrel)
        {

        }

        

        //Get Editions
        public async Task<List<Edition>> GetEditions()
        {
            var editionsResult = await GetAsync<EditionsList>("index.json", Connectivity.NetworkAccess, 6); //Cache for 6 hours

            return editionsResult.Editions;
        }

        //Get Edition details with Articles
        public async Task<ArticlesList> GetEditionWithArticles(string editionId)
        {
            var articlesResult = await GetAsync<ArticlesList>($"{editionId}.json", Connectivity.NetworkAccess, 30 * 24); //Cache for 30 days
                        
            return articlesResult;
        }


        //Get Articles for Edition
        public async Task<List<Article>> GetArticalForEdition(string editionId)
        {
            var articlesResult = await GetEditionWithArticles(editionId); 

            return articlesResult.Articles;
        }
    }
}
