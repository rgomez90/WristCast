using Xamarin.Forms;

namespace WristCast.Controls
{
    /// <summary>
    /// ProgressOptions types
    /// </summary>
    public enum ProgressOptions
    {
        Small,
        Large,
    }

    /// <summary>
    /// Progress for Circle
    /// </summary>
    public class CircleProgress : View
    {
        /// <summary>
        /// ProgressOptions type property
        /// </summary>
        public static readonly BindableProperty OptionProperty = BindableProperty.Create("Option", typeof(ProgressOptions), typeof(CircleProgress), ProgressOptions.Small);

        /// <summary>
        /// ProgressOptions type
        /// </summary>
        public ProgressOptions Option
        {
            get => (ProgressOptions)GetValue(OptionProperty);
            set => SetValue(OptionProperty, value);
        }
    }
}
