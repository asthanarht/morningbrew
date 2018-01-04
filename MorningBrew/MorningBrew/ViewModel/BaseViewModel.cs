﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MorningBrew
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				RaisePropertyChanged();
			}
		}



		public bool IsBusy
		{
			get
			{
				return isBusy;
			}
			set
			{
				isBusy = value;
				RaisePropertyChanged();
			}
		}

		public BaseViewModel()
		{

		}

		protected Page page;
		public BaseViewModel(Page page)
		{
			this.page = page;
		}


		protected void RaisePropertyChanged([CallerMemberName]  string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		string title;
		bool isBusy;

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}

