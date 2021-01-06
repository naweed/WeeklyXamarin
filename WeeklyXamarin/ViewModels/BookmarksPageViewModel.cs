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
    public class BookmarksPageViewModel : ViewModelBase
    {
        private List<Article> _articles;
        public List<Article> Articles
        {
            get => _articles;
            set => SetProperty(ref _articles, value);
        }

        public BookmarksPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appDataService)
            : base(navigationService, dialogService, appDataService)
        {
            this.Title = "BOOKMARKS";
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await LoadBookmarks();
        }

        private async Task LoadBookmarks()
        {

            SetDataLodingIndicators(true);

            try
            {
                var bookmarks = GetBookmarks();

                //Set Bookmark Flag
                SetBookmarkFlag(bookmarks.SavedArticles);

                Articles = bookmarks.SavedArticles;

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
