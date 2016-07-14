using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MorningBrew
{
	public class BrewFeedItem
	{
		public string Description { get; set; }
		public string Link { get; set; }
		public string Encoded { get; set; }

		private string publishDate;
		public string PublishDate
		{
			get { return publishDate; }
			set
			{
				DateTime time;
				if (DateTime.TryParse(value, out time))
					publishDate = time.ToLocalTime().ToString("D");
				else
					publishDate = value;
			}
		}
		public string Author { get; set; }

		public int Id { get; set; }

		public string Category { get; set; }



		private string title;
		public string Title
		{
			get
			{
				return title;
			}
			set
			{
				title = value;

			}
		}

		private string caption;

		public string Caption
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(caption))
					return caption;


				//get rid of HTML tags
				caption = Regex.Replace(Description, "<[^>]*>", string.Empty);


				//get rid of multiple blank lines
				caption = Regex.Replace(caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

				caption = caption.Substring(0, caption.Length < 200 ? caption.Length : 200).Trim() + "...";
				return caption;
			}
		}

		public string Length { get; set; }

		public List<DayBrew> DayBrewsCollection { get; set; }
	}

	public class DayBrew
	{
		public string BrewTitle { get; set; }
		public string BrewUrl { get; set; }
		public string BrewDesc { get; set; }
	}
}

