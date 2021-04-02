using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Svg.Skia.Forms.UnitTest
{
    /// <summary>
    /// Unit tests for the SvgImage class
    /// </summary>
    public class SvgImageTests
    {
        /// <summary>
        /// Resource URI for a valid image to use for testing
        /// </summary>
        private const string ResourceUri =
            "resource://Svg.Skia.Forms.UnitTest.Assets.colibri.svg?assembly=Svg.Skia.Forms.UnitTest";

        /// <summary>
        /// Sets up unit tests by initializing Xamarin.Forms
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        /// <summary>
        /// Tests SvgImage default ctor
        /// </summary>
        [Test]
        public void TestDefaultCtor()
        {
            // run
            var image = new SvgImage();

            // check
            Assert.IsNull(image.Source, "default constructed SvgImage object must have null Source");
        }

        /// <summary>
        /// Tests setting image source
        /// </summary>
        [Test]
        public void TestSetImageSource()
        {
            // set up
            var image = new SvgImage();

            var uri = new Uri(ResourceUri);
            var imageSource = ImageSource.FromUri(uri);

            // run
            image.Source = imageSource;

            // check
            Assert.IsNotNull(image.Source, "image source must have been set");
        }

        /// <summary>
        /// Tests error while setting image source
        /// </summary>
        [Test]
        public void TestErrorSettingImageSource()
        {
            // set up
            var image = new SvgImage();

            var imageSource = ImageSource.FromStream(
                (ct) => Task.FromResult<Stream>(null));

            // run
            image.Source = imageSource;

            // check
            Assert.IsNotNull(image.Source, "image source must have been set");
        }

        /// <summary>
        /// Tests setting tint color
        /// </summary>
        [Test]
        public void TestSetTintColor()
        {
            // set up
            var image = new SvgImage();

            var uri = new Uri(ResourceUri);
            var imageSource = ImageSource.FromUri(uri);
            image.Source = imageSource;

            // run
            image.TintColor = Color.Goldenrod;

            // check
            Assert.AreEqual(Color.Goldenrod, image.TintColor, "tint color must match");
        }
    }
}
