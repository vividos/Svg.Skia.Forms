namespace Svg.Skia.Forms.Sample.UWP
{
    /// <summary>
    /// UWP main page that hosts the Xamarin.Forms app
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        /// Creates a new main page
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            this.LoadApplication(new Sample.App());
        }
    }
}
