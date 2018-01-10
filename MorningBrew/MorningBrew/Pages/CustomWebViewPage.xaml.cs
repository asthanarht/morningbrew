using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MorningBrew
{
    public partial class CustomWebViewPage : BaseContentPage<BrewHomeViewModel>
    {
        
        public CustomWebViewPage(string url)
        {
            InitializeComponent();
            webView.Source = System.Uri.EscapeUriString(url);

        }

       
    }
}
