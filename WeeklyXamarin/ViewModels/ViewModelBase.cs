using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonkeyCache;
using MonkeyCache.FileStore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using WeeklyXamarin.Framework.Interfaces;
using WeeklyXamarin.Framework.UI;
using WeeklyXamarin.Models;
using WeeklyXamarin.Services;
using Xamarin.Essentials;
 
namespace WeeklyXamarin.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        //Services
        protected INavigationService _navigationService { get; private set; }
        protected IPageDialogService _pageDialogService { get; set; }
        protected IAppService _appDataService { get; set; }
        protected Bookmarks _bookMarks;

        //Properties
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value); 
        }

        private PageMode _pageMode;
        public PageMode PageMode
        {
            get => _pageMode;
            set => SetProperty(ref _pageMode, value);
        }

        private string _loadingText = string.Empty;
        public string LoadingText
        {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _dataLoaded = false;
        public bool DataLoaded
        {
            get => _dataLoaded;
            set => SetProperty(ref _dataLoaded, value);
        }

        private bool _isErrorState = false;
        public bool IsErrorState
        {
            get => _isErrorState;
            set => SetProperty(ref _isErrorState, value);
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private string _errorImage = string.Empty;
        public string ErrorImage
        {
            get => _errorImage;
            set => SetProperty(ref _errorImage, value);
        }

        protected IBarrel CacheBarrel
        {
            get => Barrel.Current;
        }

        //Commands
        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand HamburgerCommand { get; set; }
        public DelegateCommand<Article> AddBookmarkCommand { get; set; }
        public DelegateCommand<Article> RemoveBookmarkCommand { get; set; }
        public DelegateCommand<Article> OpenArticleCommand { get; set; }
        public DelegateCommand<Article> ShareArticleCommand { get; set; }


        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService, IAppService appDataService)
        {
            _navigationService = navigationService;
            _pageDialogService = dialogService;
            _appDataService = appDataService;

            NavigateBackCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync());
            CloseCommand = new DelegateCommand(async () => await _navigationService.GoBackAsync(useModalNavigation: PageMode == PageMode.Modal));
            HamburgerCommand = new DelegateCommand(async () => await _pageDialogService.DisplayAlertAsync("TODO", "Implemention of Animated Hamburger button with menu to be done!", "CLOSE"));

            AddBookmarkCommand = new DelegateCommand<Article>(AddBookmark);
            RemoveBookmarkCommand = new DelegateCommand<Article>(RemoveBookmark);
            OpenArticleCommand = new DelegateCommand<Article>(OpenArticle);
            ShareArticleCommand = new DelegateCommand<Article>(ShareArticle);


            IsErrorState = false;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        //Set Loading Indicators for Page
        protected void SetDataLodingIndicators(bool isStaring = true)
        {
            if (isStaring)
            {
                IsBusy = true;
                DataLoaded = false;
                IsErrorState = false;
                ErrorMessage = "";
                ErrorImage = "";
            }
            else
            {
                LoadingText = "";
                IsBusy = false;
            }
        }

        //Get Bookmarked Articles
        protected Bookmarks GetBookmarks()
        {
            if (_bookMarks != null)
                return _bookMarks;

            //Get Bookmarks from Cache if possible
            if (CacheBarrel.Exists(Constants.BookmarksKey))
            {
                _bookMarks = CacheBarrel.Get<Bookmarks>(Constants.BookmarksKey);
            }
            else
                _bookMarks = new Bookmarks();

            return _bookMarks;
        }

        //Save Bookmarked Articles
        private async void SaveBookmarks(Bookmarks bookmarks)
        {
            CacheBarrel.Add(Constants.BookmarksKey, bookmarks, TimeSpan.FromDays(5 * 365)); //Save for 5 years

            await Task.CompletedTask;
        }

        //Add Bookmark
        private async void AddBookmark(Article article)
        {
            var bookmarks = GetBookmarks();

            article.IsBookmarked = true;

            bookmarks.Add(article);

            SaveBookmarks(bookmarks);

            //Show Toast Message
            Xamarin.Forms.DependencyService.Get<IToastMessage>().ShortAlert("The article has been saved in Bookmarks!");

            await Task.CompletedTask;
        }

        //Remove Bookmark
        private async void RemoveBookmark(Article article)
        {
            var bookmarks = GetBookmarks();

            article.IsBookmarked = false;

            bookmarks.Remove(article);

            SaveBookmarks(bookmarks);

            //Show Toast Message
            Xamarin.Forms.DependencyService.Get<IToastMessage>().ShortAlert("Bookmark has been removed!");

            await Task.CompletedTask;
        }

        //Set Bookmarks Flag for all articles
        protected async void SetBookmarkFlag(List<Article> articles)
        {
            var bookmarks = GetBookmarks();

            articles.ForEach(article => { SetBookmarkFlag(article); });

            await Task.CompletedTask;
        }

        //Set Bookmarks Flag for individual article
        protected async void SetBookmarkFlag(Article article)
        {
            var bookmarks = GetBookmarks();

            if (bookmarks.SavedArticles.Any(bookmark => bookmark.Id == article.Id))
                article.IsBookmarked = true; 

            await Task.CompletedTask;
        }


        //Open Article
        private async void OpenArticle(Article article)
        {
            await Browser.OpenAsync(article.Url, new BrowserLaunchOptions { LaunchMode = BrowserLaunchMode.SystemPreferred, TitleMode = BrowserTitleMode.Show });
        }

        //Share Article
        private async void ShareArticle(Article article)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = article.Url,
                Title = article.Title,
                Subject = article.Title,
                Text = "Hi, I found this wonderful article on Xamarin. Thought you might like it. Enjoy!"
            });
        }


    }
}
