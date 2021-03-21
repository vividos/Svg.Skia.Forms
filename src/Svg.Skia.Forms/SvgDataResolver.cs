using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Svg.Skia.Forms
{
    /// <summary>
    /// Resolves loading SVG image data from various image sources
    /// </summary>
    internal static class SvgDataResolver
    {
        /// <summary>
        /// Loads SVG image from given image source
        /// </summary>
        /// <param name="source">image source to use for loading</param>
        /// <returns>loaded SVG image object</returns>
        /// <exception cref="Exception">thrown when loading has failed</exception>
        public static async Task<SKSvg> LoadSvgImage(ImageSource source)
        {
            switch (source)
            {
                case StreamImageSource streamSource:
                    return await LoadImageFromStream(streamSource);
                case UriImageSource uriImageSource:
                    return LoadImageFromUri(uriImageSource.Uri);
                case FileImageSource fileImageSource:
                    return LoadImageFromFile(fileImageSource.File);
            }

            return null;
        }

        /// <summary>
        /// Loads SVG image from stream image source
        /// </summary>
        /// <param name="streamSource">stream image source</param>
        /// <returns>loaded SVG image object, or null when loading was not successful</returns>
        private static async Task<SKSvg> LoadImageFromStream(StreamImageSource streamSource)
        {
            var stream = await streamSource.Stream(CancellationToken.None);

            if (stream == null)
            {
                throw new InvalidOperationException("StreamImageSource stream is null");
            }

            var svg = new SKSvg();
            svg.Load(stream);

            return svg;
        }

        /// <summary>
        /// Loads image from an URI
        /// </summary>
        /// <param name="uri">resource or data URI</param>
        /// <returns>loaded SVG image object, or null when loading was not successful</returns>
        private static SKSvg LoadImageFromUri(Uri uri)
        {
            if (uri.Scheme == "data")
            {
                return LoadImageFromDataUri(uri);
            }

            throw new NotSupportedException("URI not supported: " + uri.OriginalString);
        }

        /// <summary>
        /// Loads image from data URI
        /// </summary>
        /// <param name="uri">data URI</param>
        /// <returns>loaded SVG image object, or null when loading was not successful</returns>
        private static SKSvg LoadImageFromDataUri(Uri uri)
        {
            string text = uri.OriginalString;

            string svgText = null;
            if (text.StartsWith(SvgConstants.DataUriPlainPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                svgText = text.Substring(SvgConstants.DataUriPlainPrefix.Length);
            }
            else if (text.StartsWith(SvgConstants.DataUriBase64Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                svgText = text.Substring(SvgConstants.DataUriBase64Prefix.Length);

                var base64EncodedBytes = Convert.FromBase64String(svgText);
                svgText = Encoding.UTF8.GetString(base64EncodedBytes);
            }

            if (svgText == null)
            {
                throw new FormatException("Invalid SVG image text format: " + uri.OriginalString);
            }

            var svg = new SKSvg();
            svg.FromSvg(svgText);

            return svg;
        }

        /// <summary>
        /// Loads image from filename
        /// </summary>
        /// <param name="filename">image filename</param>
        /// <returns>loaded SVG image object, or null when loading was not successful</returns>
        private static SKSvg LoadImageFromFile(string filename)
        {
            var svg = new SKSvg();
            svg.Load(filename);

            return svg;
        }
    }
}
