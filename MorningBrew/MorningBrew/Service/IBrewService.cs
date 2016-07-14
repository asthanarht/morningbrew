using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MorningBrew
{
	public interface IBrewService
	{
		Task<IEnumerable<BrewFeedItem>> GetBrewsAsync();
	}
}

