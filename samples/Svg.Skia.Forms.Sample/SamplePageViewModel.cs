using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Svg.Skia.Forms.Sample
{
    /// <summary>
    /// View model for the sample page, providing some image sources to reference from XAML.
    /// </summary>
    public class SamplePageViewModel : ObservableObject
    {
        /// <summary>
        /// Test SVG image as text; see also shapes.svg
        /// </summary>
        private const string TestSvgImageText =
            @"<svg width=""64"" height=""64"" xmlns=""http://www.w3.org/2000/svg"">
  <rect x=""10"" y=""15"" width=""20"" height=""30"" style=""fill:rgb(0,255,0)"" />
  <circle cx=""45"" cy=""40"" r=""15"" stroke=""black"" stroke-width=""3"" fill=""red"" />
  <path fill=""#ff0"" stroke-width=""2"" stroke=""green"" d=""M60 60 H30 L15 30 Z"" />
</svg>";

        /// <summary>
        /// Backing store for dynamic image size
        /// </summary>
        private double dynamicImageSize = 64.0;

        #region Binding properties
        /// <summary>
        /// Image source from stream coming from the app package's Assets folder
        /// </summary>
        public ImageSource ImageFromPlatformAssets { get; }

        /// <summary>
        /// Image source from a Forms based assmbly, integrated with EmbeddedResource
        /// </summary>
        public ImageSource ImageFromFormsAssets { get; }

        /// <summary>
        /// Data URL containing base64 encoded SVG image
        /// </summary>
        public string SvgImageDataUrlBase64Encoded { get; } =
            SvgConstants.DataUriBase64Prefix + EncodeBase64(TestSvgImageText);

        /// <summary>
        /// Data URL containing unencoded SVG image
        /// </summary>
        public string SvgImageDataUrlUnencoded { get; }
            = SvgConstants.DataUriPlainPrefix + TestSvgImageText;

        /// <summary>
        /// Plain SVG image data
        /// </summary>
        public string SvgImagePlainData { get; } = TestSvgImageText;

        /// <summary>
        /// Command to execute when user clicked on the "Pick SVG image" button
        /// </summary>
        public ICommand PickSvgImageCommand { get; }

        /// <summary>
        /// Image source of picked image
        /// </summary>
        public ImageSource PickedImage { get; private set; }

        /// <summary>
        /// Current image size for dynamic image resizing
        /// </summary>
        public double DynamicImageSize
        {
            get => this.dynamicImageSize;
            set => this.SetProperty(ref this.dynamicImageSize, value);
        }
        #endregion

        /// <summary>
        /// Creates a new view model object and initializes all binding properties
        /// </summary>
        public SamplePageViewModel()
        {
            this.ImageFromPlatformAssets = ImageSource.FromStream(
                () => GetPlatformStream());

            this.ImageFromFormsAssets = ImageSource.FromResource(
                "Svg.Skia.Forms.Sample.Assets.toucan.svg",
                typeof(SamplePageViewModel).Assembly);

            this.PickSvgImageCommand = new AsyncCommand(this.PickSvgImage);
        }

        /// <summary>
        /// Returns a stream from the platform's assets folder
        /// </summary>
        /// <returns>platform file stream</returns>
        private static Stream GetPlatformStream()
        {
            string filename = "Assets/toucan.svg";

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                filename = filename.Replace("Assets/", string.Empty);
            }

            return FileSystem.OpenAppPackageFileAsync(filename).Result;
        }

        /// <summary>
        /// Encodes SVG image text as Base64
        /// </summary>
        /// <param name="svgImageText">image text</param>
        /// <returns>Base64 encoded text</returns>
        private static string EncodeBase64(string svgImageText)
        {
            var utf8bytes = Encoding.UTF8.GetBytes(svgImageText);
            return Convert.ToBase64String(utf8bytes);
        }

        /// <summary>
        /// Lets the user pick an SVG image from storage and tries to display it
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task PickSvgImage()
        {
            try
            {
                var options = new PickOptions
                {
                    FileTypes = new FilePickerFileType(
                        new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { DevicePlatform.Android, new string[] { "image/svg+xml" } },
                            { DevicePlatform.UWP, new string[] { ".svg" } },
                            { DevicePlatform.iOS, null },
                        }),
                    PickerTitle = "Select an SVG image to display"
                };

                var result = await FilePicker.PickAsync(options);

                if (result == null ||
                    string.IsNullOrEmpty(result.FullPath))
                {
                    return;
                }

                var stream = await result.OpenReadAsync();
                this.PickedImage = ImageSource.FromStream(() => stream);

                this.OnPropertyChanged(nameof(this.PickedImage));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert(
                   "Svg.Skia.Forms Sample",
                   "Error while picking an SVG image file: " + ex.Message,
                   "Close");
            }
        }
    }
}
