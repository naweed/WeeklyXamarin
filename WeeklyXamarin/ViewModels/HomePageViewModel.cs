using System;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using WeeklyXamarin.Framework.Exceptions;
using WeeklyXamarin.Models;
using WeeklyXamarin.Services;

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


        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appDataService)
            : base(navigationService, dialogService, appDataService)
        {
            this.Title = "Weekly Xamarin";
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await LoadLatestArticles();
        }

        private async Task LoadLatestArticles()
        {
            this.LoadingText = "Preparing latest Xamarin content.";
            
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
            }
            catch (InternetConnectionException iex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Slow or no internet connection." + Environment.NewLine + "Please check you internet connection and try again.";
                ////TODO: Add the NoInternet Image
                ErrorImage = "nointernet.png";
            }
            catch (Exception ex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Something went wrong. If the problem persists, plz contact support at Apps@xgeno.com with the error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
                ////TODO: Add the Error Image
                ErrorImage = "error.png";
            }
            finally
            {
                SetDataLodingIndicators(false);
            }
        }

        
    }
}
