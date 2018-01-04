using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MorningBrew.ViewModel;
using Xamarin.Forms;

namespace MorningBrew.Code
{
	public enum BrewPage
	{
		BrewFeed,
		FavoriteBrew,
		BrewSetting
	}
	public class RootPageAndroid : MasterDetailPage
	{
		Dictionary<int, BrewNavigationPage> pages;
		public RootPageAndroid()
		{
			this.pages = new Dictionary<int, BrewNavigationPage>();
			Master = new MenuPage(this);
		}

		public async Task NavigateAsync(int menuId)
		{
			BrewNavigationPage newPage = null;
				//only cache specific pages
				switch (menuId)
				{
				case (int)BrewPage.BrewFeed: //Feed
					pages.Add(menuId, new BrewNavigationPage(new BrewsHome()));
						break;
				case (int)BrewPage.FavoriteBrew://sessions
					pages.Add(menuId, new BrewNavigationPage(new BrewFavoritePage()));
						break;
					
				}


			if (newPage == null)
				newPage = pages[menuId];

			if (newPage == null)
				return;

			//if we are on the same tab and pressed it again.
			if (Detail == newPage)
			{
				await newPage.Navigation.PopToRootAsync();
			}

			Detail = newPage;
		}
	}


}

