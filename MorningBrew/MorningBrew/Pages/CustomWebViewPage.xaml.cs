using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MorningBrew
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
            this.Brew = brew;

                webView.Source = System.Uri.EscapeUriString(brew.BrewUrl);
                webView.Navigating+= (sender, e) => {
                    using (var b = new Busy(ViewModel, "One moment, Loading"))
                    {
                    Task.Delay(1000);
                    }
                };


        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
           

        }

        async void Bookmarked_Clicked(object sender, EventArgs e)
        {
            var m = await ViewModel.ExecuteFavoriteCommandAsync(this.Brew);
            Hud.Instance.ShowToast("This feature has not been implemented yet.");
        }
    }
}
