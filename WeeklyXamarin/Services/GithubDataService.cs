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
            var editionsResult = await GetAsync<EditionsResult>(Constants.EditionsIndexResourceURL, Connectivity.NetworkAccess, 6); //Cache for 6 hours

            return editionsResult.Editions;
        }

        //Get Edition details with Articles
        public async Task<Edition> GetEditionDetails(string editionId)
        {
            var articlesResult = await GetAsync<Edition>(Constants.EditionResourceURL.Replace("<<EditionId>>", editionId), Connectivity.NetworkAccess, 30 * 24); //Cache for 30 days
                        
            return articlesResult;
        }

        //Search for articles
        public async IAsyncEnumerable<Article> SearchArticles(string searchTerm)
        {
            //Get all editions
            var editions = await GetEditions();

            //Get articles for each edition
            foreach(var edition in editions)
            {
                var editionDetails = await GetEditionDetails(edition.Id);

                //Check for Match with Search Term and return the article
                foreach(var article in editionDetails.Articles)
                {
                    if(article.ToString().Contains(searchTerm.ToLower()))
                        yield return article;
                }
            }
        }
    }
}
