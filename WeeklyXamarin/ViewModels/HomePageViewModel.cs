using System;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WeeklyXamarin.Framework.Exceptions;
using WeeklyXamarin.Models;
using WeeklyXamarin.Services;
using WeeklyXamarin.Views;

namespace WeeklyXamarin.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing; 
            set => SetProperty(ref _isRefreshing, value);
        }

        private Edition _latestEdition;
        public Edition LatestEdition
        {
            get => _latestEdition;
            set => SetProperty(ref _latestEdition, value);
        }

        public DelegateCommand<string> SearchArticlesCommand { get; set; }
        public DelegateCommand NavigateToEditionsPageCommand { get; set; }
        public DelegateCommand NavigateToBookmarksPageCommand { get; set; }
        public event EventHandler DownloadCompleted;

        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appDataService)
            : base(navigationService, dialogService, appDataService)
        {
            this.Title = "LATEST ARTICLES";

            SearchArticlesCommand = new DelegateCommand<string>(SearchArticles);
            NavigateToEditionsPageCommand = new DelegateCommand(NavigateToEditionsPage);
            NavigateToBookmarksPageCommand = new DelegateCommand(NavigateToBookmarksPage);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            _bookMarks = null;
            await LoadLatestArticles();
        }

        private async Task LoadLatestArticles()
        {
            this.LoadingText = "Preparing latest Xamarin content";
            
            SetDataLodingIndicators(true);

            try
            {
                //Get Latest Edition
                var editions = await _appDataService.GetEditions();

                //Get Articles for Latest Edition
                var edition = await _appDataService.GetEditionDetails(editions.OrderByDescending(e => e.Id).FirstOrDefault().Id);

                //Set Bookmark Flag
                SetBookmarkFlag(edition.Articles);

                LatestEdition = edition;

                this.DataLoaded = true;

                //Raise the Event to notify the page of download completion
                DownloadCompleted?.Invoke(this, new EventArgs());

                //Preload Editions
                foreach(var preloadEdition in editions.Where(e => e.Id != edition.Id))
                {
                    _ = _appDataService.GetEditionDetails(preloadEdition.Id);
                }
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

        //Search Articles Page
        private async void SearchArticles(string searchTerm)
        {
            await _navigationService.NavigateAsync($"{nameof(SearchResultsPage)}?searchTerm={searchTerm}", useModalNavigation: false);
        }

        //Navigate to Bookmarks Page
        private async void NavigateToBookmarksPage()
        {
            await _navigationService.NavigateAsync($"{nameof(BookmarksPage)}", useModalNavigation: false);
        }

        //Navigate to Editions Page
        private async void NavigateToEditionsPage()
        {
            await _navigationService.NavigateAsync($"{nameof(EditionsPage)}", useModalNavigation: false);
        }


    }
}
