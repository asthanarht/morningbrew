using System;

using Xamarin.Forms;

namespace MorningBrew.Code
{
	public class WebViewPage : ContentPage
	{
		public WebViewPage(string title, string url)
		{
			NavigationPage.SetHasNavigationBar(this, true);
			NavigationPage.SetBackButtonTitle(this, title);
			this.Title = title;
			WebView webView = new WebView
			{
				Source = new UrlWebViewSource
				{
					Url = url,
				},
				VerticalOptions = LayoutOptions.FillAndExpand
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
	}
}


