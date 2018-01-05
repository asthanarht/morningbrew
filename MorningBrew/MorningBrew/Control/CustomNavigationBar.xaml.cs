using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MorningBrew
{
    public partial class CustomNavigationBar : ContentView
    {
        public CustomNavigationBar()
        {
            InitializeComponent();
            root.BindingContext = this;
        }
    }
}
