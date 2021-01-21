using System.Collections.Generic;
using WeeklyXamarin.ViewModels;
using Xamarin.Forms;
using WeeklyXamarin.Framework.UI;


namespace WeeklyXamarin.Views
{
    public partial class PageBase : ContentPage
    {
        public IList<View> PageContent => PageContentGrid.Children;
        public IList<View> PageIcons => PageIconsGrid.Children;

        protected bool IsBackButtonEnabled
        {
            set => NavigateBackButton.IsEnabled = value;
        }

        protected bool IsPhoneX
        {
            get
            {
                var deviceInfo = Xamarin.Essentials.DeviceInfo.Model;

                return (Xamarin.Essentials.DeviceInfo.Platform == Xamarin.Essentials.DevicePlatform.iOS && (deviceInfo == "iPhone10,3" || deviceInfo == "iPhone10,6" || deviceInfo.StartsWith("iPhone11,") || deviceInfo.StartsWith("iPhone12,") || deviceInfo.StartsWith("iPhone13,") || deviceInfo.StartsWith("iPhone14,")));
            }
        }

        #region Bindable properties
        public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(nameof(PageTitle), typeof(string), typeof(PageBase), string.Empty, defaultBindingMode: BindingMode.OneWay, propertyChanged: OnPageTitleChanged);

        public static readonly BindableProperty PageModeProperty = BindableProperty.Create(nameof(PageMode), typeof(PageMode), typeof(PageBase), PageMode.None, propertyChanged: OnPageModePropertyChanged);

        public string PageTitle
        {
            get => (string)GetValue(PageTitleProperty);
            set => SetValue(PageTitleProperty, value);
        }

        public PageMode PageMode
        {
            get => (PageMode)GetValue(PageModeProperty);
            set => SetValue(PageModeProperty, value);
        }



        private static void OnPageTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && bindable is PageBase basePage)
                basePage.TitleLabel.Text = (string) newValue;
        }

        private static void OnPageModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null && bindable is PageBase basePage)
                basePage.SetPageMode((PageMode) newValue);
        }
        #endregion

        public PageBase()
        {
            InitializeComponent();

            //Fix Top Margin Height
            StatusRowDefinition.Height = DependencyService.Get<IDeviceInfo>().StatusbarHeight;

            //Set Page Mode
            SetPageMode(PageMode.None);
        }

        protected override void OnAppearing()
        {

        }

        private void SetPageMode(PageMode pageMode)
        {
            if (BindingContext != null)
            {
                ((ViewModelBase)BindingContext).PageMode = pageMode;

                switch (pageMode)
                {
                    case PageMode.Menu:
                        HamburgerButton.IsVisible = true;
                        NavigateBackButton.IsVisible = false;
                        CloseButton.IsVisible = false;
                        break;
                    case PageMode.Navigate:
                        HamburgerButton.IsVisible = false;
                        NavigateBackButton.IsVisible = true;
                        CloseButton.IsVisible = false;
                        break;
                    case PageMode.Modal:
                        HamburgerButton.IsVisible = false;
                        NavigateBackButton.IsVisible = false;
                        CloseButton.IsVisible = true;
                        break;
                    default:
                        HamburgerButton.IsVisible = false;
                        NavigateBackButton.IsVisible = false;
                        CloseButton.IsVisible = false;
                        break;
                }
            }
        }

    }

    
}
