using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;

namespace MorningBrew
{
	public class BrewHomeViewModel:BaseViewModel
	{
		public ObservableRangeCollection<BrewFeedItem> BrewFeed { get; set; } 
		     = new ObservableRangeCollection<BrewFeedItem>();

		public ObservableRangeCollection<Grouping<string, DayBrew>> BrewFeedGrouped { get; } 
		         = new ObservableRangeCollection<Grouping<string, DayBrew>>();

		private bool isFavorite;
		public bool IsFavorite
		{
			get
			{
				return isFavorite;
			}
			set
			{
				isFavorite = value;
				//RaisePropertyChanged();
			}
		}

		readonly IBrewService brewService;
		readonly IBrewFavoriteService  brewFavService;

		public BrewHomeViewModel()
		{
			brewService = DependencyService.Get<IBrewService>();
			brewFavService = DependencyService.Get<IBrewFavoriteService>();
		}

		ICommand loadBrewsCommand;
		public ICommand LoadBrewsCommand =>
		loadBrewsCommand ?? (loadBrewsCommand = new Command<bool>(async (f) => await ExecuteLoadBrewsAsync()));

		public async Task ExecuteLoadBrewsAsync()
		{
            using (var b = new Busy(this, "Wait for loading"))
            {
                this.isFavorite = true;
                BrewFeed.AddRange(await brewService.GetBrewsAsync());
                Sort();
            }

		}

		private void Sort()
		{

			BrewFeedGrouped.Clear();
			var sorted = from brew in BrewFeed
				select new Grouping<string, DayBrew>(brew.Title, brew.DayBrewsCollection);

			BrewFeedGrouped.ReplaceRange(sorted);
		}

		ICommand favoriteCommand;
		public ICommand FavoriteCommand =>
		favoriteCommand ?? (favoriteCommand = new Command<DayBrew>(async (s) => await ExecuteFavoriteCommandAsync(s)));

		public async Task ExecuteFavoriteCommandAsync(DayBrew brew)
		{
			var isFav=  await brewFavService.InsertFavoritBrew(brew);
			if (isFav)
				App.current.ShowToast(new Plugin.Toasts.NotificationOptions
				{ 
				 Title = "Morning Brew",
					Description = "Your Brew is favorited",
					IsClickable = true,

					ClearFromHistory = false,
					DelayUntil = DateTime.Now.AddSeconds(10)
			});
			//var toggled = await FavoriteService.ToggleFavorite(session);
			//if (toggled && Settings.Current.FavoritesOnly)
			//	SortSessions();
		}

}
}

