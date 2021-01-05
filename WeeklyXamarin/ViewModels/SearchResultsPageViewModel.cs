using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WeeklyXamarin.Framework.Exceptions;
using WeeklyXamarin.Models;
using WeeklyXamarin.Services;

namespace WeeklyXamarin.ViewModels
{
    public class SearchResultsPageViewModel : ViewModelBase
    {
        private ObservableCollection<Article> _articles;
        public ObservableCollection<Article> Articles
        {
            get => _articles;
            set => SetProperty(ref _articles, value);
        }

        public SearchResultsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appDataService)
            : base(navigationService, dialogService, appDataService)
        {
            this.Title = "SEARCH RESULTS";

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("searchTerm"))
            {
                await SearchArticles(parameters["searchTerm"].ToString());
            }
        }

        private async Task SearchArticles(string searchTerm)
        {
            this.LoadingText = "Searching for related articles";

            SetDataLodingIndicators(true);

            try
            {
                Articles = new ObservableCollection<Article>();

                //Get Search Articales
                var articles = _appDataService.SearchArticles(searchTerm);

                await foreach (Article article in articles)
                {
                    //Set Bookmark Flag
                    SetBookmarkFlag(article);

                    Articles.Add(article);
                }


                this.DataLoaded = true;
            }
            catch (InternetConnectionException iex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Slow or no internet connection." + Environment.NewLine + "Please check you internet connection and try again.";
                ErrorImage = "nointernet.png";
            }
            catch (Exception ex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Something went wrong. If the problem persists, plz contact support at Apps@xgeno.com with the error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
                ErrorImage = "error.png";
            }
            finally
            {
                SetDataLodingIndicators(false);
            }
        }

    }
}
