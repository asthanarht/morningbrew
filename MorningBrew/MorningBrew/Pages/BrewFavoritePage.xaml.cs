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
			

			//FavListViewBrew.ItemSelected += async (sender, e) =>
				//{

				//	var brew = FavListViewBrew.SelectedItem as DayBrew;
				//	if (brew == null)
				//		return;

				//	await Navigation.PushAsync(new WebViewPage(brew.BrewTitle, brew.BrewUrl));


				//	FavListViewBrew.SelectedItem = null;
				//};
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
           

            if (ViewModel.FavBrewFeed.Count == 0)
                ViewModel.LoadFavBrewsCommand.Execute(false);

        }
		//protected override void OnAppearing()
		//{
		//	base.OnAppearing();

		//	if (vm.FavBrewFeed.Count == 0)
		//		vm.LoadFavBrewsCommand.Execute(false);
		//}
	}
}

