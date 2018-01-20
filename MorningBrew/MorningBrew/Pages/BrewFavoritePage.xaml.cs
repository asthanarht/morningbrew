using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MorningBrew.ViewModel
{
    public partial class BrewFavoritePage : BaseContentPage<BrewFavoriteViewModel>
	{
		public BrewFavoritePage()
		{
			InitializeComponent();
			
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.FavBrewFeed.Count == 0)
                ViewModel.LoadFavBrewsCommand.Execute(false);

        }
        async void FavBrewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
                var brew = FavListViewBrew.SelectedItem as DayBrew;
                if (brew == null)
                    return;
                await Navigation.PushAsync(new CustomWebViewPage(brew));
                //  await Navigation.PushModalAsync(ToNav(new CustomWebViewPage(brew)));


                FavListViewBrew.SelectedItem = null;

        }
        async void BrewDeletedClicked(object sender, EventArgs e)
        {
            var svg = sender as SvgImage;
            var t = svg.BindingContext as DayBrew;
            var ok = await DisplayAlert("Are you sure you want to delete this brew?", "", "Yes", "No");

            if (!ok)
                return;
            ViewModel.DeleteFavBrewsCommand.Execute(t);

            Hud.Instance.ShowToast("Brew is deleted",NoticationType.Error);
        }
	}
}

