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
            Hud.Instance.ShowToast("test");

            if (ViewModel.FavBrewFeed.Count == 0)
                ViewModel.LoadFavBrewsCommand.Execute(false);

        }

        async void BrewDeletedClicked(object sender, EventArgs e)
        {
            
            Hud.Instance.ShowToast("Brew Deleted Called");
        }
	}
}

