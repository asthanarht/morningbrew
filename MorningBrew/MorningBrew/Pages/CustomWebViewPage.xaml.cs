using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MorningBrew
{
    public partial class CustomWebViewPage : BaseContentPage<BrewHomeViewModel>
    {
        public DayBrew Brew
        {
            get;
            set;
        }
        public CustomWebViewPage()
        {
            InitializeComponent();
        }

        public CustomWebViewPage(DayBrew brew)
        {
            InitializeComponent();
            this.Brew = brew;
            webView.Source = System.Uri.EscapeUriString(brew.BrewUrl);

        }


        async void Bookmarked_Clicked(object sender, EventArgs e)
        {
            var m = await ViewModel.ExecuteFavoriteCommandAsync(this.Brew);
        }
    }
}
