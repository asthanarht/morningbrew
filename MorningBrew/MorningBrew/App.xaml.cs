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

			switch (Device.OS)
			{
				case TargetPlatform.Android:
					MainPage = new NavigationPage(new BrewsHome());
					break;
				case TargetPlatform.iOS:
					MainPage = new RootPageiOS();
					break;
				case TargetPlatform.Windows:
				case TargetPlatform.WinPhone:
					MainPage = new NavigationPage(new BrewsHome());
					break;
				default:
					throw new NotImplementedException();
			}

		}

		public async void ShowToast(INotificationOptions options)
		{
			var notificator = DependencyService.Get<IToastNotificator>();
			var result = await notificator.Notify(options);
		}
	}
}

