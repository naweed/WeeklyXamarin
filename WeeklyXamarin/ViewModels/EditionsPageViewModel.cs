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
    public class EditionsPageViewModel : ViewModelBase
    {
        private List<Edition> _editions;
        public List<Edition> Editions
        {
            get => _editions;
            set => SetProperty(ref _editions, value);
        }

        public EditionsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appDataService)
            : base(navigationService, dialogService, appDataService)
        {
            this.Title = "EDITIONS";
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await LoadEditions();
        }

        private async Task LoadEditions()
        {
            this.LoadingText = "Getting latest editions";

            SetDataLodingIndicators(true);

            try
            {
                Editions = await _appDataService.GetEditions();

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
