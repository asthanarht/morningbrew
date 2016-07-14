using System;
using System.Collections.Generic;
using MorningBrew.ViewModel;
using Xamarin.Forms;

namespace MorningBrew
{
	public partial class App : Application
	{
		public static App current;
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new BrewsHome());
		}
	}
}

