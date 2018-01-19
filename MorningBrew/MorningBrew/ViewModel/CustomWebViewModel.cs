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
        public DayBrew Brew
        {
            get;
            set;
        }
        public ICommand AddCommand { get; private set; }
        readonly IBrewFavoriteService brewFavService;
        public CustomWebViewModel()
		{
            brewFavService = DependencyService.Get<IBrewFavoriteService>();
            AddCommand = new  Command(async () =>
            {
                using (var b = new Busy(this, "Wait for loading"))
                {
                    var isFav = await brewFavService.InsertFavoritBrew(this.Brew);
                    if(isFav)
                        Hud.Instance.ShowToast("Brew is bookmarked.",NoticationType.Success);
                    else
                        Hud.Instance.ShowToast("Brew is already bookmarked.");
                   
                }
            });
		}


        ICommand favBrewsCommand;
        public ICommand FavBrewsCommand =>
        favBrewsCommand ?? (favBrewsCommand = new Command<bool>(async (f) => await ExecuteFavoriteCommandAsync()));

       
        public async Task<bool> ExecuteFavoriteCommandAsync()
        {

            using (var b = new Busy(this, "Wait for loading"))
            {
                var isFav = await brewFavService.InsertFavoritBrew(this.Brew);
                Hud.Instance.ShowToast("Brew is bookmarked.");
                return isFav;
            }
            //var toggled = await FavoriteService.ToggleFavorite(session);
            //if (toggled && Settings.Current.FavoritesOnly)
            //  SortSessions();
        }
     }
}

