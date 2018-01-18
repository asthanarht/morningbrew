using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace MorningBrew
{
	public class BrewFavoriteViewModel:BaseViewModel
	{
		public ObservableRangeCollection<DayBrew> FavBrewFeed { get; set; }
			 = new ObservableRangeCollection<DayBrew>();

		readonly IBrewFavoriteService brewFavService;
		public BrewFavoriteViewModel()
		{
			brewFavService = DependencyService.Get<IBrewFavoriteService>();
		}

		ICommand loadFavBrewsCommand;
		public ICommand LoadFavBrewsCommand =>
		loadFavBrewsCommand ?? (loadFavBrewsCommand = new Command<bool>(async (f) => await ExecuteFavLoadBrewsAsync()));

		public async Task ExecuteFavLoadBrewsAsync()
		{
			
			FavBrewFeed.AddRange(await brewFavService.GetFavoriteBrew());

		}
	}
}

