using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeeklyXamarin.Views
{
    public partial class HomePage : PageBase
    {
        private bool _isSearchBarOpen = false;
        private double _originalLeft;
        private double _originalWidth;

        public HomePage()
        {
            InitializeComponent();
        }

        private void ToggleSearchBar(object sender, EventArgs e)
        {
            if (!_isSearchBarOpen)
            {
                btnSearchArticles.Text = "";

                OpenSearchBox();

                _isSearchBarOpen = true;

                btnSearchArticles.Focus();
            }
            else
            {
                CloseSearchBox();

                _isSearchBarOpen = false;

                SearchButton.Unfocus();
            }
        }

        private void OpenSearchBox()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                MenuLayout.TranslateTo(0, 0);

                AnimateBlueBarIn().ContinueWith(t =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MenuLayout.FadeTo(1);
                    });
                });
            });
        }

        private void CloseSearchBox()
        {
            MenuLayout.TranslateTo(0, 0, 20);
            MenuLayout.FadeTo(0.0001, 20);


            AnimateBlueBarOut().ContinueWith(t =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    MenuLayout.TranslateTo(0, MenuLayout.Height * -1, 0);
                });
            });
        }

        private Task AnimateBlueBarIn()
        {
            var r = BarLayout.Bounds;
            _originalLeft = r.Left;
            _originalWidth = r.Width;

            r.Width = MenuLayout.Width + 5;
            r.Left = r.Left - ((Width - _originalWidth) / 2);
            return BarLayout.LayoutTo(r, 150);
        }

        private Task AnimateBlueBarOut()
        {
            var r = BarLayout.Bounds;
            r.Width = _originalWidth;
            r.Left = _originalLeft;
            return BarLayout.LayoutTo(r, 150);
        }

        private async void btnSearchArticles_SearchButtonPressed(object sender, EventArgs e)
        {
            //string searchTerm = (btnSearchShows.Text != null ? btnSearchShows.Text.Trim() : "");

            //ToggleActionBarMenu(null, null);

            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    searchTerm = System.Net.WebUtility.HtmlEncode(searchTerm);

            //    (this.BindingContext as DiscoverPageViewModel).SearchShowsCommand.Execute(searchTerm);
            //}
            //else
            //{
            //    (this.BindingContext as DiscoverPageViewModel).SearchShowsCommand.Execute("");
            //}

            //await Task.CompletedTask;
        }
    }
}
