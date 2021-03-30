using NUnit.Framework;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Svg.Skia.Forms.UnitTest
{
    /// <summary>
    /// Unit tests for the SvgDataResolver class
    /// </summary>
    public class SvgDataResolverTests
    {
        /// <summary>
        /// Tests calling LoadSvgImage() with a null image source; this must result in a null
        /// SKSvg instance being returned. No exception is thrown.
        /// </summary>
        /// <returns>task to wait on</returns>
        [Test]
        public async Task TestLoadSvgImage_NullArgument()
        {
            // run
            var svg = await SvgDataResolver.LoadSvgImage(null);

            // check
            Assert.IsNull(svg, "loaded SKSvg object must be null");
        }
    }
}
