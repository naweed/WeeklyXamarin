using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using WeeklyXamarin.Framework.Exceptions;
using WeeklyXamarin.Models;
using WeeklyXamarin.Services;

namespace WeeklyXamarin.ViewModels
{
    public class EditionDetailsPageViewModel : ViewModelBase
    {
        private Edition edition;
        public Edition Edition
        {
            get => edition;
            set => SetProperty(ref edition, value);
        }


        public EditionDetailsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appDataService)
            : base(navigationService, dialogService, appDataService)
        {
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("editionId"))
            {
                var editionId = parameters["editionId"].ToString();

                this.Title = $"EDITION {editionId}";

                await LoadEdition(editionId);
            }
        }

        private async Task LoadEdition(string editionId)
        {
            this.LoadingText = "Preparing latest Xamarin content";

            SetDataLodingIndicators(true);

            try
            {
                //Get Edition Details
                var edition = await _appDataService.GetEditionDetails(editionId);

                //Set Bookmark Flag
                SetBookmarkFlag(edition.Articles);

                Edition = edition;

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
