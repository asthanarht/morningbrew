using System;
using Xamarin.Forms;

namespace MorningBrew.Code
{
	public class BrewNavigationPage: NavigationPage
    {
        public BrewNavigationPage(Page root) : base(root)
        {
			//Init();
			Title = root.Title;
			Icon = root.Icon;
	     }

		public BrewNavigationPage()
		{
			//Init();
		}		

		void Init()
		{
			if (Device.OS == TargetPlatform.iOS)
			{
				BarBackgroundColor = Color.FromHex("FAFAFA");
			}
			else
			{
				BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
				BarTextColor = (Color)Application.Current.Resources["NavigationText"];
			}
		}
}
}

