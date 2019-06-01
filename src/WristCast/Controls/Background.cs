using Xamarin.Forms;

namespace WristCast.Controls
{
    /// <summary>
    /// Types of Background Option
    /// </summary>
    public enum BackgroundOptions
    {
        /// <summary>
        /// Center the background image.
        /// </summary>
        Center,

        /// <summary>
        /// Scale the background image, retaining the aspect ratio.
        /// </summary>
        Scale,

        /// <summary>
        /// Stretch the background image to fill the widget's area.
        /// </summary>
        Stretch,

        /// <summary>
        /// Tile the background image at its original size.
        /// </summary>
        Tile
    }

    /// <summary>
    /// Background view
    /// </summary>
    public class Background : View
    {
        /// <summary>
        /// Image source property
        /// </summary>
        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(FileImageSource), typeof(Background), default(FileImageSource));

        /// <summary>
        /// BackgroundOptions type property
        /// </summary>
        public static readonly BindableProperty OptionProperty = BindableProperty.Create("Option", typeof(BackgroundOptions), typeof(Background), BackgroundOptions.Scale);

        /// <summary>
        /// Image source
        /// </summary>
        public UriImageSource Image
        {
            get => (UriImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        /// <summary>
        /// BackgroundOptions type
        /// </summary>
        public BackgroundOptions Option
        {
            get => (BackgroundOptions)GetValue(OptionProperty);
            set => SetValue(OptionProperty, value);
        }
    }
}
