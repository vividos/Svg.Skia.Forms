using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Svg.Skia.Forms.Sample
{
    /// <summary>
    /// Xamarin.Forms based sample application
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Creates a new app object
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            this.MainPage = new SamplePage();
        }
    }
}
