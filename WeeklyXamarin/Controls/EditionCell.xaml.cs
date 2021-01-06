﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WeeklyXamarin.Controls
{
    public partial class EditionCell : ContentView
    {
        public static readonly BindableProperty ParentContextProperty = BindableProperty.Create("ParentContext", typeof(object), typeof(ArticleCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as EditionCell).ParentContext = newValue;
            }
        }

        public EditionCell()
        {
            InitializeComponent();
        }
    }
}
