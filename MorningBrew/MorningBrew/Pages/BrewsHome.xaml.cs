﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lottie.Forms;
using MorningBrew;
using MorningBrew.Code;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MorningBrew.ViewModel
{
    public partial class BrewsHome : BaseContentPage<BrewHomeViewModel>
    {
		
		public BrewsHome()
		{
            InitializeComponent();


		}
        async void BrewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            using (var b = new Busy(ViewModel, "One moment, please"))
            {
                var brew = ListViewBrew.SelectedItem as DayBrew;
                if (brew == null)
                    return;
                await Navigation.PushAsync(new CustomWebViewPage(brew));
              //  await Navigation.PushModalAsync(ToNav(new CustomWebViewPage(brew)));


                ListViewBrew.SelectedItem = null;
            }
        }
        async void BookMarkClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BrewFavoritePage());
           // Hud.Instance.ShowToast("This is in home page and bookmark called");
        }

     

		 void OnTapGestureRecognizerTapped(object sender, EventArgs args)
		{
			Image imageSender = (Image)sender;
			var brew = (DayBrew)imageSender.BindingContext;
			// vm.FavoriteCommand.Execute(brew);
		}
		void ListViewTapped(object sender, ItemTappedEventArgs e)
		{
			
			var list = sender as ListView;
			if (list == null)
				return;
			list.SelectedItem = null;

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			ListViewBrew.ItemTapped += ListViewTapped;

                if (ViewModel.BrewFeed.Count == 0)
                    ViewModel.LoadBrewsCommand.Execute(false);
            
		}

        public static NavigationPage ToNav( ContentPage page)
        {
            return new NavigationPage(page)
            {
                BarTextColor = Color.White,
            };
        }

		//public static readonly BindableProperty FavoriteCommandProperty =
		//	BindableProperty.Create(nameof(FavoriteCommand), typeof(ICommand), typeof(DayBrew), default(ICommand));

		//public ICommand FavoriteCommand
		//{
		//	get { return GetValue(FavoriteCommandProperty) as Command; }
		//	set { SetValue(FavoriteCommandProperty, value); }
		//}


    }
}
