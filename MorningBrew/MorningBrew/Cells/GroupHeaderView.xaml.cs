using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MorningBrew
{
	public class GroupHeader : ViewCell
	{
		public GroupHeader()
		{
			View = new GroupHeaderView();
		}
	}
	public partial class GroupHeaderView : ContentView
	{
		public GroupHeaderView()
		{
			InitializeComponent();
		}
	}
}

