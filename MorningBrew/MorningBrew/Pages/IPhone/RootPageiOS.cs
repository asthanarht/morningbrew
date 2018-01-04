using System;
using MorningBrew.ViewModel;
using Xamarin.Forms;
namespace MorningBrew.Code
{
	public class RootPageiOS:TabbedPage
	{
		public RootPageiOS()
		{
			this.Children.Add(new BrewNavigationPage (new BrewsHome()));
			this.Children.Add(new BrewNavigationPage (new BrewFavoritePage()));
		}
	}
}

