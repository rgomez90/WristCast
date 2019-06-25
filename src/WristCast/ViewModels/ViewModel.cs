using System;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using WristCast.Core.Shared;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public abstract class ViewModel : BindableBase
    {

        public virtual Task Init() { return Task.CompletedTask; }
        public virtual Task Clean() { return Task.CompletedTask; }

        protected static InformationPopup CreateProgressPopup(string text, string title = null)
        {
            var progressPopUp = CreateTextPopup(text, title, null, false);
            progressPopUp.IsProgressRunning = true;
            return progressPopUp;
        }

        protected static InformationPopup CreateTextButtonPopup(string text, string buttonText, Action buttonAction, string title = null, Action backButtonAction = null, bool dismissOnBackButtonPressed = true)
        {
            var textButtonPopUp = CreateTextPopup(text, title, backButtonAction, dismissOnBackButtonPressed);
            var bottomButon = new MenuItem
            {
                Text = buttonText,
                Command = new Command(buttonAction)
            };
            textButtonPopUp.BottomButton = bottomButon;
            return textButtonPopUp;
        }

        protected static InformationPopup CreateTextPopup(string text, string title = null, Action backButtonAction = null, bool dismissOnBackButtonPressed = true)
        {
            var textPopUp = new InformationPopup { Text = text };
            if (title != null)
            {
                textPopUp.Title = title;
            }

            if (dismissOnBackButtonPressed)
            {
                textPopUp.BackButtonPressed += (s, e) => textPopUp.Dismiss();
            }

            if (backButtonAction != null)
            {
                textPopUp.BackButtonPressed += (s, e) => backButtonAction.Invoke();

            }
            return textPopUp;
        }



        protected virtual void ShowToast(string text, FileImageSource iconMediaSource = null, int duration = 3)
        {
            if (iconMediaSource == null)
            {
                Toast.DisplayText(text, duration);
            }
            else
            {
                Toast.DisplayIconText(text, iconMediaSource, duration);
            }
        }
    }

    public abstract class ViewModel<T> : ViewModel
    {
        public abstract void Prepare(T parameter);
    }
}