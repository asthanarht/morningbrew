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

            //var bg = new Gradient()
            //{
            //    Rotation = 150,
            //    Steps = new GradientStepCollection()
            //    {
            //        new GradientStep((Color)Application.Current.Resources["topGradient"], 0),
            //        new GradientStep((Color)Application.Current.Resources["bottomGradient"], 1)
            //    }
            //};

            NavigationPage.SetHasNavigationBar(this, false);
            //ContentPageGloss.SetBackgroundGradient(this, bg);


            if (Hud.Instance == null)
                Hud.Instance = this;


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

            AbsoluteLayout.SetLayoutFlags(_contentView, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(_contentView, new Rectangle(0, 0, 1, 1));

            AbsoluteLayout.SetLayoutFlags(_hudRoot, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(_hudRoot, new Rectangle(0, 0, 1, 1));



            _rootLayout.Children.Add(_contentView);
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

