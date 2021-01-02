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

        public ErrorIndicator()
        {
            InitializeComponent();
        }
    }
}
