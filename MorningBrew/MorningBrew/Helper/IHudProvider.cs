using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MorningBrew
{
    public interface IHudProvider
    {
        string HudMessage { get; set; }

        void ShowProgress(string message);
        void Show(string message, View view = null);
        Task Dismiss(bool animate = false);
    }

    public static class Hud
    {
        public static IHudProvider Instance { get; set; }
    }

    public enum NoticationType
    {
        None,
        Info,
        Success,
        Error,
    }
}
