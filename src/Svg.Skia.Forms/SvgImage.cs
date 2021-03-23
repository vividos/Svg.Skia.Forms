using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Svg.Skia.Forms
{
    /// <summary>
    /// SVG image control for Xamarin.Forms, using the Svg.Skia NuGet package.
    /// </summary>
    public class SvgImage : SKCanvasView
    {
        /// <summary>
        /// Lazy-initialized SVG image instance
        /// </summary>
        private SKSvg svgImage;

        /// <summary>
        /// Creates a new SVG image control
        /// </summary>
        public SvgImage()
        {
            this.BackgroundColor = Color.Transparent;
            this.PaintSurface += this.CanvasViewOnPaintSurface;
        }

        #region Bindable properties
        /// <summary>
        /// Source property, storing an ImageSource instance holding the SVG image
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(ImageSource),
            typeof(SvgImage),
            default(ImageSource),
            propertyChanged: OnSourcePropertyChanged);

        /// <summary>
        /// Image source for SVG image. The image source can be any image source created by
        /// ImageSource factory methods (e.g. FromStream()), but can also be a string that is
        /// converted by the SvgImageSourceTypeConverter converter.
        /// </summary>
        [TypeConverter(typeof(SvgImageSourceTypeConverter))]
        public ImageSource Source
        {
            get => (ImageSource)this.GetValue(SourceProperty);
            set => this.SetValue(SourceProperty, value);
        }
        #endregion

        /// <summary>
        /// Called when the Source property has been changed.
        /// </summary>
        /// <param name="bindable">bindable object</param>
        /// <param name="oldValue">old bound value</param>
        /// <param name="newValue">newly bound value</param>
        private static void OnSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is SvgImage image))
            {
                return;
            }

            Task.Run(async () =>
            {
                try
                {
                    image.svgImage = await SvgDataResolver.LoadSvgImage(image.Source);
                    Device.BeginInvokeOnMainThread(image.InvalidateSurface);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SvgImage.Source: Error while loading image: " + ex.ToString());
                }
            });
        }

        /// <summary>
        /// Called when binding context has changed
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            if (this.Source != null)
            {
                BindableObject.SetInheritedBindingContext(this.Source, this.BindingContext);
            }

            base.OnBindingContextChanged();
        }

        /// <summary>
        /// Called in order to paint on the surface of the SkiaSharp canvas.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            if (this.svgImage == null)
            {
                return;
            }

            SKImageInfo info = args.Info;
            canvas.Translate(info.Width / 2f, info.Height / 2f);

            SKRect bounds = this.svgImage.Picture.CullRect;
            float xRatio = info.Width / bounds.Width;
            float yRatio = info.Height / bounds.Height;

            float ratio = Math.Min(xRatio, yRatio);

            canvas.Scale(ratio);
            canvas.Translate(-bounds.MidX, -bounds.MidY);

            canvas.DrawPicture(this.svgImage.Picture);
        }
    }
}
