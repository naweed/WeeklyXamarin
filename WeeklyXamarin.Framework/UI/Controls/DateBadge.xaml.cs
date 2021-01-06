using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WeeklyXamarin.Framework.UI.Controls
{
    public partial class DateBadge : ContentView
    {
        public static readonly BindableProperty DateProperty = BindableProperty.Create("Date", typeof(DateTime), typeof(DateBadge), null, propertyChanged: OnDatePropertyChanged);

        public DateTime Date
        {
            get { return Convert.ToDateTime(GetValue(DateProperty)); }
            set { SetValue(DateProperty, value); }
        }

        private static void OnDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                var control = bindable as DateBadge;
                var date = (DateTime)newValue;
                control.lblYear.Text = date.Year.ToString();
                control.lblMonth.Text = date.ToString("MMM");
                control.lblDay.Text = date.Day.ToString("00");
            }
        }

        public DateBadge()
        {
            InitializeComponent();
        }
    }
}
