using System;
using System.Collections.Generic;
using System.Windows.Input;
using MorningBrew.Code;
using Xamarin.Forms;

namespace MorningBrew.Cells
{
	public partial class BrewCell : ViewCell
	{
		readonly INavigation navigation;
		public BrewCell(INavigation navigation = null)
		{
			Height = 120;
			View = new BrewCellView();
			this.navigation = navigation;

		}

		protected override async void OnTapped()
		{
			base.OnTapped();
			if (navigation == null)
				return;
			var brew = BindingContext as DayBrew;
			if (brew == null)
				return;


			await NavigationService.PushAsync(navigation, new WebViewPage(brew.BrewTitle, brew.BrewUrl));
		}
	}

	public partial class BrewCellView : ContentView
	{
		public BrewCellView()
		{
			InitializeComponent();

		}


		public static readonly BindableProperty FavoriteCommandProperty =
			BindableProperty.Create(nameof(FavoriteCommand), typeof(ICommand), typeof(BrewCellView), default(ICommand));

		public ICommand FavoriteCommand
		{
			get { return GetValue(FavoriteCommandProperty) as Command; }
			set { SetValue(FavoriteCommandProperty, value); }
		}
	}
}

