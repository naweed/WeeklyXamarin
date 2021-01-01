using System;
using Prism.Navigation;
using Prism.Services;
using WeeklyXamarin.Services;

namespace WeeklyXamarin.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appDataService)
            : base(navigationService, dialogService, appDataService)
        {
            this.Title = "Weekly Xamarin";
        }
    }
}
