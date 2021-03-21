using Xamarin.Forms;

namespace Svg.Skia.Forms.Sample
{
    /// <summary>
    /// Xamarin.Forms page to show sample images
    /// </summary>
    public partial class SamplePage : ContentPage
    {
        /// <summary>
        /// Creates a new sample page
        /// </summary>
        public SamplePage()
        {
            this.BindingContext = new SamplePageViewModel();
            this.InitializeComponent();
        }
    }
}
