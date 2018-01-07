using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MorningBrew
{
    public class BaseViewModel : BaseNotify
	{
        public event EventHandler IsBusyChanged;
        bool _isBusy;
        public virtual bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (SetPropertyChanged(ref _isBusy, value))
                {
                    SetPropertyChanged(nameof(IsNotBusy));
                    OnIsBusyChanged();
                    IsBusyChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        public bool IsNotBusy => !IsBusy;

        protected virtual void OnIsBusyChanged() { }


        string _currentState;
        public string CurrentState
        {
            get { return _currentState; }
            set { SetPropertyChanged(ref _currentState, value); }
        }
	}
}

