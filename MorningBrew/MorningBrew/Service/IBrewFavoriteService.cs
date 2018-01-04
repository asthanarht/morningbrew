using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MorningBrew
{
	public interface IBrewFavoriteService
	{
		Task<List<DayBrew>> GetFavoriteBrew();
		Task<bool> InsertFavoritBrew(DayBrew brewFeed);
		//Task UpdateCompanies();
	}
}

