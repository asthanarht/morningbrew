using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MorningBrew.ViewModel
{
    public partial class CustomWebViewPage : BaseContentPage<CustomWebViewModel>
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
            this.Brew= ViewModel.Brew = brew;

                webView.Source = System.Uri.EscapeUriString(brew.BrewUrl);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }

        //async void Bookmarked_Clicked(object sender, EventArgs e)
        //{
        //    var m = await ViewModel.ExecuteFavoriteCommandAsync(this.Brew);
        //    Hud.Instance.ShowToast("This feature has not been implemented yet.");
        //}
    }
}
