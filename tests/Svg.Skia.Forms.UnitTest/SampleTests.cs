using NUnit.Framework;
using Svg.Skia.Forms.Sample;

namespace Svg.Skia.Forms.UnitTest
{
    /// <summary>
    /// Tests for the sample project
    /// </summary>
    public class SampleTests
    {
        /// <summary>
        /// Sets up unit tests by initializing Xamarin.Forms
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        /// <summary>
        /// Tests creating sample page view model
        /// </summary>
        [Test]
        public void TestSamplePageViewModel()
        {
            // set up
            var viewModel = new SamplePageViewModel();

            // check
            Assert.IsNotNull(viewModel.SvgImageDataUrlBase64Encoded, "property must be non-null");
        }

        /// <summary>
        /// Tests creating sample page
        /// </summary>
        [Test]
        public void TestSamplePage()
        {
            // set up
            _ = new Xamarin.Forms.Application();
            var page = new SamplePage();

            // check
            Assert.IsNotNull(page.BindingContext, "binding context must be non-null");
        }
    }
}
