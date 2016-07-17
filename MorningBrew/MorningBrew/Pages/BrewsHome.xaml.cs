using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MorningBrew.Cells;
using MorningBrew.Code;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MorningBrew.ViewModel
{
    public partial class BrewsHome : ContentPage
    {
		BrewHomeViewModel vm;
		public BrewsHome()
		{
			InitializeComponent();
			BindingContext = vm = new BrewHomeViewModel();


			ListViewBrew.ItemTapped += (sender, e) => ListViewBrew.SelectedItem = null;
			ListViewBrew.ItemSelected += async (sender, e) =>
				{
				var brew = ListViewBrew.SelectedItem as DayBrew;
					if (brew == null)
						return;

				await Navigation.PushAsync(new WebViewPage(brew.BrewTitle,brew.BrewUrl));


					ListViewBrew.SelectedItem = null;
				};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (vm.BrewFeed.Count == 0)
				vm.LoadBrewsCommand.Execute(false);
		}


    }
}
