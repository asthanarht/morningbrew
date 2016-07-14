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

		readonly IBrewService brewService;

		public BrewHomeViewModel()
		{
			brewService = DependencyService.Get<IBrewService>();
		}

		ICommand loadBrewsCommand;
		public ICommand LoadBrewsCommand =>
		loadBrewsCommand ?? (loadBrewsCommand = new Command<bool>(async (f) => await ExecuteLoadBrewsAsync()));

		public async Task ExecuteLoadBrewsAsync()
		{
			BrewFeed.AddRange(await brewService.GetBrewsAsync());
			Sort();

		}

		private void Sort()
		{

			BrewFeedGrouped.Clear();
			var sorted = from brew in BrewFeed
				select new Grouping<string, DayBrew>(brew.Title, brew.DayBrewsCollection);

			BrewFeedGrouped.ReplaceRange(sorted);
		}

}
}

