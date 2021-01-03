using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WeeklyXamarin.Controls
{
    public partial class ErrorIndicator : StackLayout
    {
        //Bindable Properties
        public static BindableProperty IsErrorProperty = BindableProperty.Create("IsError", typeof(bool), typeof(ErrorIndicator), false, BindingMode.OneWay, null, SetIsError);

        public bool IsError
        {
            get { return (bool)this.GetValue(IsErrorProperty); }
            set { this.SetValue(IsErrorProperty, value); }
        }

        private static void SetIsError(BindableObject bindable, object oldValue, object newValue)
        {
            ErrorIndicator control = bindable as ErrorIndicator;

            control.IsVisible = (bool)newValue;
        }

        public static BindableProperty ErrorTextProperty = BindableProperty.Create("ErrorText", typeof(string), typeof(ErrorIndicator), "", BindingMode.OneWay, null, SetErrorText);

        public string ErrorText
        {
            get { return (string)this.GetValue(ErrorTextProperty); }
            set { this.SetValue(ErrorTextProperty, value); }
        }

        private static void SetErrorText(BindableObject bindable, object oldValue, object newValue)
        {
            ErrorIndicator control = bindable as ErrorIndicator;

            control.lblErrorText.Text = (string)newValue;
        }

        public static BindableProperty ErrorImageProperty = BindableProperty.Create("ErrorImage", typeof(ImageSource), typeof(ErrorIndicator), null, BindingMode.OneWay, null, SetErrorImage);

        public ImageSource ErrorImage
        {
            get { return (ImageSource)this.GetValue(ErrorImageProperty); }
            set { this.SetValue(ErrorImageProperty, value); }
        }

        private static void SetErrorImage(BindableObject bindable, object oldValue, object newValue)
        {
            ErrorIndicator control = bindable as ErrorIndicator;

            control.imgError.Source = (ImageSource)newValue;
        }


        public ErrorIndicator()
        {
            InitializeComponent();
        }
    }
}
