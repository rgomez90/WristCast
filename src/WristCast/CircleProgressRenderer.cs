using System.ComponentModel;
using ElmSharp;
using WristCast;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(CircleProgress), typeof(CircleProgressRenderer))]
namespace WristCast
{
    public class CircleProgressRenderer : ViewRenderer<CircleProgress, ProgressBar>
    {
        /// <summary>
        /// Called when element is changed.
        /// </summary>
        /// <param name="e">Argument for ElementChangedEventArgs<CircleProgress></param>
        protected override void OnElementChanged(ElementChangedEventArgs<CircleProgress> e)
        {
            if (Control == null)
            {
                var progressBar = new ProgressBar(Forms.NativeParent);
                SetNativeControl(progressBar);
            }

            if (e.NewElement != null)
            {
                UpdateOption();
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Called when element property is changed.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">PropertyChangedEvent</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CircleProgress.OptionProperty.PropertyName)
            {
                UpdateOption();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        /// <summary>
        /// Set style of pulsing animation
        /// </summary>
        private void UpdateOption()
        {
            Control.Style = Element.Option == ProgressOptions.Large ? "process" : "process/popup/small";
            Control.Show();
            Control.PlayPulse();
        }
    }
}
