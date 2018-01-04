using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MorningBrew;
using Newtonsoft.Json;
using PCLStorage;
using Xamarin.Forms;

[assembly: Dependency(typeof(BrewFavoriteService))]
namespace MorningBrew
{
	public class BrewFavoriteService: IBrewFavoriteService
	{

		private const string BrewFolder = "MorningBrew";
		private const string BrewFileName = "brew.json";
		private static List<DayBrew> favListItems;
		public BrewFavoriteService()
		{
			
		}

		public async Task<List<DayBrew>> GetFavoriteBrew()
		{
			var folder = await NavigateToFolder(BrewFolder);

			if ((await folder.CheckExistsAsync(BrewFileName)) == ExistenceCheckResult.NotFound)
			{
				return new List<DayBrew>();
			}
			IFile file = await folder.GetFileAsync(BrewFileName);
			var jsonCompanies = await file.ReadAllTextAsync();

			if (string.IsNullOrEmpty(jsonCompanies)) return new List<DayBrew>();

			var brews = JsonConvert.DeserializeObject<List<DayBrew>>(jsonCompanies);

			return brews;

		}

		public async Task<bool> InsertFavoritBrew(DayBrew brewFeed)
		{
			try
			{

				var brewList = await GetFavoriteBrew();

				brewList.Add(brewFeed);
				var folder = await NavigateToFolder(BrewFolder);
				return await SaveBrewList(folder,brewList);
			}
			catch
			{
				return false;
			}
		}

		private async Task<bool> SaveBrewList(IFolder folder,List<DayBrew> brewList)
		{
			IFile file = await folder.CreateFileAsync(BrewFileName, CreationCollisionOption.ReplaceExisting);
			var brewString = JsonConvert.SerializeObject(brewList);
			await file.WriteAllTextAsync(brewString);
			return true;
		}

		private static async Task<IFolder> NavigateToFolder(string targetFolder)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync(targetFolder,
				CreationCollisionOption.OpenIfExists);

			return folder;
		}
	}
}

