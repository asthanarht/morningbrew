using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MorningBrew.Code
{
	public partial class BrewFavoritePage : ContentPage
	{
		BrewFavoriteViewModel vm;

		public BrewFavoritePage()
		{
			InitializeComponent();
			BindingContext = vm = new BrewFavoriteViewModel();

			FavListViewBrew.ItemSelected += async (sender, e) =>
				{

					var brew = FavListViewBrew.SelectedItem as DayBrew;
					if (brew == null)
						return;

					await Navigation.PushAsync(new WebViewPage(brew.BrewTitle, brew.BrewUrl));


					FavListViewBrew.SelectedItem = null;
				};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (vm.FavBrewFeed.Count == 0)
				vm.LoadFavBrewsCommand.Execute(false);
		}
	}
}

