using System;
using System.Threading.Tasks;
using Lottie.Forms;
using Xamarin.Forms;

namespace MorningBrew
{
    public class BaseContentPage<T> : ContentPage, IHudProvider where T : BaseViewModel, new()
    {
        public BaseContentPage()
        {
            _viewModel = new T();
            Initialize();
        }
        public BaseContentPage(T viewModel)
        {
            _viewModel = viewModel;
            Initialize();
        }

        AbsoluteLayout _rootLayout; //Root container for all controls on page, including HUD
        Grid _hudRoot; //Root container for all HUD related controls
        Label _hudLabel; //Text displayed to user when HUD is showing
        ContentView _hudView; //Holds a view, such as an animation or checkmark image
        AnimationView _progressAnimation; //Progress animation that plays when HUD is showing
       //Root container for all Toast related controls
        Grid _toastRoot;
        Label _toastLabel; 
        public string HudMessage { 
            get { return _hudLabel?.Text; }
            set { if (_hudLabel == null) return; _hudLabel.Text = value; }
        }

        T _viewModel;
        public T ViewModel
        {
            get { return _viewModel; }
            protected set { _viewModel = value; }
        }

        View _contentView;
        public View RootContent
        {
            get { return _contentView; }
            set { _contentView = value; if (value != null) SetContent(); }
        }

        void Initialize()
        {
            BindingContext = _viewModel;
            NavigationPage.SetHasNavigationBar(this, false);

            if (Hud.Instance == null)
                Hud.Instance = this;
            

        }
      
        protected override void OnAppearing()
        {
            Hud.Instance = this;
           // SetContent();
            base.OnAppearing();
        }
        private void SetContent()
        {
            if (_contentView == null)
                return;

            _rootLayout = new AbsoluteLayout();
            _hudRoot = new Grid();

            var bg = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };

            var stack = new StackLayout
            {
                Padding = 30,
                Spacing = 30,
                VerticalOptions = LayoutOptions.Center,
            };

            _hudLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            _hudView = new ContentView
            {
                HorizontalOptions = LayoutOptions.Center,
            };

            _progressAnimation = new AnimationView
            {
                Animation = "progress_balls.json",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = 100,
                HeightRequest = 60,
                Loop = true,
            };

            stack.Children.Add(_hudView);
            stack.Children.Add(_hudLabel);
            bg.Content = stack;

            _hudRoot.Children.Add(bg);
            _hudRoot.BackgroundColor = (Color)Application.Current.Resources["hudBackgroundColor"];
            _hudRoot.IsVisible = false;

            _toastRoot = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(0),
                BackgroundColor = (Color)Application.Current.Resources["toastBackgroundColor"],
                IsVisible = false,
                HeightRequest = CustomNavigationBar.YOffset,
            };

            var separatorBottom = new ContentView { Style = (Style)Application.Current.Resources["separator"] };
            var separatorTop = new ContentView { Style = (Style)Application.Current.Resources["separator"] };
            separatorTop.VerticalOptions = LayoutOptions.Start;

            _toastLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.WordWrap,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(20, 10 + _toastTopMargin, 20, 10),
            };

            _toastRoot.Children.Add(separatorTop);
            _toastRoot.Children.Add(separatorBottom);
            _toastRoot.Children.Add(_toastLabel);


            AbsoluteLayout.SetLayoutFlags(_contentView, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(_contentView, new Rectangle(0, 0, 1, 1));

            AbsoluteLayout.SetLayoutFlags(_hudRoot, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(_hudRoot, new Rectangle(0, 0, 1, 1));


            AbsoluteLayout.SetLayoutFlags(_toastRoot, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(_toastRoot, new Rectangle(0, 0, 1, 1));

            _toastRoot.TranslationY = CustomNavigationBar.YOffset * -1;

            _rootLayout.Children.Add(_contentView);
            _rootLayout.Children.Add(_toastRoot);
            _rootLayout.Children.Add(_hudRoot);

            Content = _rootLayout;
        }

        public async Task Dismiss(bool animate = false)
        {
            if (_hudRoot == null)
                return;

            if (animate)
            {
                await Task.Delay(300);
                await _hudRoot.FadeTo(0, 300);
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                if (_progressAnimation != null)
                    _progressAnimation.Loop = false;

                _hudRoot.IsVisible = false;
            });
        }
        double _toastTopMargin = Device.RuntimePlatform == Device.iOS ? 20 : 0;
        double _toastHeight = CustomNavigationBar.YOffset;

        public async void ShowToast(string message, NoticationType type, int timeout = 3500)
        {
            switch(type)
            {
                case NoticationType.Success:
                    {
                        _toastRoot.BackgroundColor = Color.Green;
                        break;
                    }
                case NoticationType.Error:
                    {
                        _toastRoot.BackgroundColor = Color.Red;
                        break;
                    }
                case NoticationType.None:
                    {
                        _toastRoot.BackgroundColor = (Color)Application.Current.Resources["toastBackgroundColor"];
                        break;
                    }
            }
            if (_toastRoot == null || _toastRoot.IsVisible)
                return;

            if (_hudRoot != null && _hudRoot.IsVisible)
                await Dismiss();

            Device.BeginInvokeOnMainThread(() =>
            {
                _toastLabel.Text = message;
                _toastRoot.IsVisible = true;
            });

            await _toastRoot.TranslateTo(0, 0, 250);
            await Task.Delay(timeout).ConfigureAwait(false);
            await _toastRoot.TranslateTo(0, CustomNavigationBar.YOffset * -1, 250);

            Device.BeginInvokeOnMainThread(() =>
            {
                _toastRoot.IsVisible = false;
            });
        }

      
        public void Show(string message, View view = null)
        {

            if (_hudRoot == null)
                return;

            _hudView.IsVisible = view != null;
            _hudLabel.Text = message;
            _hudLabel.IsVisible = !string.IsNullOrWhiteSpace(message);
            _progressAnimation.Loop = false;

            if (view != null)
                _hudView.Content = view;

            _hudRoot.IsVisible = true;
        }

        public void ShowProgress(string message)
        {
            if (_hudRoot == null)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                _hudView.Content = _progressAnimation;
                _hudLabel.Text = message;
                _hudRoot.IsVisible = true;
                _hudLabel.IsVisible = !string.IsNullOrWhiteSpace(message);
                _hudView.IsVisible = true;
                _progressAnimation.Loop = true;
                _progressAnimation.Speed = 1;
                _progressAnimation.Play();
            });
        }

       
    }
}

