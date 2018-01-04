using System;

using Xamarin.Forms;

namespace MorningBrew.Code
{
	public class WebViewPage : ContentPage
	{
		public WebViewPage(string title, string url)
		{
			try
			{
				NavigationPage.SetHasNavigationBar(this, true);
				NavigationPage.SetBackButtonTitle(this, title);
				this.Title = title;
				WebView webView = new WebView
				{

					Source = System.Uri.EscapeUriString(url),

					VerticalOptions = LayoutOptions.FillAndExpand,

					HorizontalOptions = LayoutOptions.FillAndExpand
				};

				// Build the page.
				this.Content = new StackLayout
				{
					Children =
				{
					webView
				}
				};
			}
			catch
			{
			}
		
		}
	}
}


