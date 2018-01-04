using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MorningBrew.Code
{
	public partial class MenuPage : ContentPage
	{
		RootPageAndroid androidPage;
		public MenuPage(RootPageAndroid androidPage)
		{
			this.androidPage = androidPage;
			InitializeComponent();

			AndroidNavView.NavigationItemSelected += (sender, e) =>
			{
				this.androidPage.IsPresented = false;
				Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
				{
					Device.BeginInvokeOnMainThread(async () =>
						{
							await this.androidPage.NavigateAsync(e.Index);
						});
					return false;
				});
			};
		}
	}
}

