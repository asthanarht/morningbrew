using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MorningBrew;
using MorningBrew.Code;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MorningBrew.ViewModel
{
    public partial class BrewsHome : BaseContentPage<BrewHomeViewModel>
    {
		
		public BrewsHome()
		{
			 InitializeComponent();
           // var t = this.animationViewCustom.AutoPlay=true;
           
            //AnimationViewCustom.Focused +=  (sender, e) =>
            //{
                
            //    var brew = ListViewBrew.SelectedItem as DayBrew;

            //     if (brew != null)
            //    {
            //         ViewModel.FavoriteCommand.Execute(brew);
            //    }
                    
            //    var m = sender;
            //    var t = e;
            //    var tss = "safsaf";
            //};
			//BindingContext = vm = new BrewHomeViewModel();

           // ViewModel.ExecuteLoadBrewsAsync().GetAwaiter().GetResult();

			//ListViewBrew.ItemSelected += async (sender, e) =>
				//{
				
				//var brew = ListViewBrew.SelectedItem as DayBrew;
				//	if (brew == null)
				//		return;

				//await Navigation.PushAsync(new WebViewPage(brew.BrewTitle,brew.BrewUrl));


				//	ListViewBrew.SelectedItem = null;
				//};


		}
        async void BookMarkClicked(object sender, EventArgs e)
        {
        }


		 void OnTapGestureRecognizerTapped(object sender, EventArgs args)
		{
			Image imageSender = (Image)sender;
			var brew = (DayBrew)imageSender.BindingContext;
			// vm.FavoriteCommand.Execute(brew);
		}
		void ListViewTapped(object sender, ItemTappedEventArgs e)
		{
			
			var list = sender as ListView;
			if (list == null)
				return;
			list.SelectedItem = null;

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			ListViewBrew.ItemTapped += ListViewTapped;

                if (ViewModel.BrewFeed.Count == 0)
                    ViewModel.LoadBrewsCommand.Execute(false);
            
		}

		//public static readonly BindableProperty FavoriteCommandProperty =
		//	BindableProperty.Create(nameof(FavoriteCommand), typeof(ICommand), typeof(DayBrew), default(ICommand));

		//public ICommand FavoriteCommand
		//{
		//	get { return GetValue(FavoriteCommandProperty) as Command; }
		//	set { SetValue(FavoriteCommandProperty, value); }
		//}


    }
}
