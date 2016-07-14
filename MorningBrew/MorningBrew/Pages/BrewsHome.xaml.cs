using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (vm.BrewFeed.Count == 0)
				vm.LoadBrewsCommand.Execute(false);
		}
    }
}
