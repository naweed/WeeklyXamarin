using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using WeeklyXamarin.Views;
using WeeklyXamarin.ViewModels;
using WeeklyXamarin.Services;
using WeeklyXamarin.Models;
using MonkeyCache.FileStore;

[assembly: ExportFont("IBMPlexSans-Medium.ttf", Alias = "MediumFont")]
[assembly: ExportFont("IBMPlexSans-Regular.ttf", Alias = "RegularFont")]
[assembly: ExportFont("IBMPlexSans-Light.ttf", Alias = "LightFont")]

namespace WeeklyXamarin
{
    public partial class App
    {
        protected override IContainerExtension CreateContainerExtension() => PrismContainerExtension.Current;

        public App()
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //Navigate to Start Page
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(HomePage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Initialze MonkeyCache
            ////TODO: Not the best place to do this. Need to figure out how to set this in App Initialization (rather than Containers Registration)
            Barrel.ApplicationId = Constants.BarrelApplicationId;

            //Navigation Service
            containerRegistry.RegisterForNavigation<NavigationPage>();

            ////Other Services
            //App Rest Service to connect to Github data
            containerRegistry.RegisterInstance<IAppService>(new GithubDataService(Constants.GitHubURL, Barrel.Current));

            //Views and View Models
            containerRegistry.RegisterForNavigation<PageBase>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<SearchResultsPage, SearchResultsPageViewModel>();
            containerRegistry.RegisterForNavigation<BookmarksPage, BookmarksPageViewModel>();
            containerRegistry.RegisterForNavigation<EditionsPage, EditionsPageViewModel>();
        }

    }
}
