using System;
using Xamarin.Forms.Platform.Android;
using Android.Support.Design.Widget;
using Android.Runtime;
using Xamarin.Forms;



using Android.Widget;

using Android.Views;
using MorningBrew.Droid;

[assembly: ExportRenderer(typeof(MorningBrew.NavigationView), typeof(NavigationViewRenderer))]
namespace MorningBrew.Droid
{
	public class NavigationViewRenderer:ViewRenderer<MorningBrew.NavigationView,NavigationView>
	{
		NavigationView navView;
		ImageView profileImage;

		protected override void OnElementChanged(ElementChangedEventArgs<MorningBrew.NavigationView> e)
		{

			base.OnElementChanged(e);
			if (e.OldElement != null || Element == null)
				return;


			var view = Inflate(Forms.Context, Resource.Layout.nav_view, null);
			navView = view.JavaCast<NavigationView>();


			navView.NavigationItemSelected += NavView_NavigationItemSelected;

			//Settings.Current.PropertyChanged += SettingsPropertyChanged;
			SetNativeControl(navView);







			navView.SetCheckedItem(Resource.Id.nav_feed);
		}




		IMenuItem previousItem;

		void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{


			if (previousItem != null)
				previousItem.SetChecked(false);

			navView.SetCheckedItem(e.MenuItem.ItemId);

			previousItem = e.MenuItem;

			int id = 0;
			switch (e.MenuItem.ItemId)
			{
				case Resource.Id.nav_feed:
					id = 0;
					break;
				case Resource.Id.nav_Favorite:
					id = 1;
					break;
				case Resource.Id.nav_settings:
					id = 1;
					break;

			}
			this.Element.OnNavigationItemSelected(new  NavigationItemSelectedEventArgs
			{

				Index = id
			});
		}
	}
}

