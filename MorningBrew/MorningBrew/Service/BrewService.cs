using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using MorningBrew;
using Xamarin.Forms;

[assembly: Dependency(typeof(BrewService))]
namespace MorningBrew
{
	public class BrewService : IBrewService
	{
		private List<BrewFeedItem> brewFeedItem = new List<BrewFeedItem>();

		public async Task<IEnumerable<BrewFeedItem>> GetBrewsAsync()
		{
			
				var httpClient = new HttpClient();
				var feed = "http://blog.cwa.me.uk/feed/rss/";
				var responseString = await httpClient.GetStringAsync(feed);

				brewFeedItem.Clear();
			return await ParseFeed(responseString);

			
		}

		private async Task<List<BrewFeedItem>> ParseFeed(string rss)
		{
			return await Task.Run(() =>
				{
					var xdoc = XDocument.Parse(rss);
				   XNamespace ns = XNamespace.Get("http://purl.org/rss/1.0/modules/content/");
					var id = 0;
					return (from item in xdoc.Descendants("item")
							select new BrewFeedItem
							{
								Title = (string)item.Element("title"),
								Description = (string)item.Element("description"),
								Link = (string)item.Element("link"),
								PublishDate = (string)item.Element("pubDate"),
								Category = (string)item.Element("category"),
					 			DayBrewsCollection = GetDaysBrewsList((string)item.Element( ns+ "encoded")),
								Id = id++
							}).ToList();
				});
		}

		private List<DayBrew> GetDaysBrewsList(string encodedString)
		{
			HtmlDocument htmlD = new HtmlDocument();
			htmlD.LoadHtml(encodedString);

			var feeds = htmlD.DocumentNode.Descendants("li");

			return (from localitem in feeds
					  let brewTitle = localitem.Descendants("a").FirstOrDefault(x => x.Attributes.Contains("href"))

					  select new DayBrew()
					  {
						  BrewTitle = WebUtility.HtmlDecode(brewTitle.InnerText),
						  BrewUrl = brewTitle.Attributes["href"].Value,
				          BrewDesc = WebUtility.HtmlDecode(localitem.InnerText)
			}).ToList();

		}
	}
}

