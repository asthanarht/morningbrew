using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;

namespace MorningBrew
{
	public class CustomWebViewModel:BaseViewModel
	{
        readonly IBrewFavoriteService brewFavService;
        public CustomWebViewModel()
		{
            brewFavService = DependencyService.Get<IBrewFavoriteService>();
		}

        public async Task<bool> ExecuteFavoriteCommandAsync(DayBrew brew)
        {
            var isFav = await brewFavService.InsertFavoritBrew(brew);

            return isFav;
            //var toggled = await FavoriteService.ToggleFavorite(session);
            //if (toggled && Settings.Current.FavoritesOnly)
            //  SortSessions();
        }
     }
}

