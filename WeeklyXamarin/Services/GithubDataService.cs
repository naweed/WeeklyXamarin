using System;
using MonkeyCache;
using MonkeyCache.FileStore;
using WeeklyXamarin.Framework.Services;
using Xamarin.Essentials;

namespace WeeklyXamarin.Services
{
    public class GithubDataService : RestServiceBase, IAppService
    {
        public GithubDataService(string apiBaseUrl, IBarrel cacheBarrel)
            : base(apiBaseUrl, cacheBarrel)
        {

        }
    }
}
