using System;
using System.Collections.Generic;
using MorningBrew.Code;
using MorningBrew.ViewModel;
using Plugin.Toasts;
using Xamarin.Forms;

namespace MorningBrew
{
	public partial class App : Application
	{
		public static App current;
		public App()
		{
			current = this;
			InitializeComponent();
			if (Current.Resources == null)
			{
				Current.Resources = new ResourceDictionary();
			}

            MainPage = new NavigationPage(new BrewsHome());

		}

		public async void ShowToast(INotificationOptions options)
		{
			var notificator = DependencyService.Get<IToastNotificator>();
			var result = await notificator.Notify(options);
		}
	}
}

