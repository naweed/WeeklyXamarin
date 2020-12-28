using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using WeeklyXamarin.Views;
using WeeklyXamarin.ViewModels;

namespace WeeklyXamarin
{
    public partial class App
    {
        protected override IContainerExtension CreateContainerExtension() => PrismContainerExtension.Current;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //Navigate to Start Page
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(HomePage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Navigation Service
            containerRegistry.RegisterForNavigation<NavigationPage>();

            //Views and View Models
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            //containerRegistry.RegisterForNavigation<BasePage>();
            //containerRegistry.RegisterForNavigation<MenuPage>();
            //containerRegistry.RegisterForNavigation<MasterDetailShellPage, MasterDetailShellPageViewModel>();
            //containerRegistry.RegisterForNavigation<WatchlistPage, WatchlistPageViewModel>();


        }

    }
}
