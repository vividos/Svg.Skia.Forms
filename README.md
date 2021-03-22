# Svg.Skia.Forms - SVG images in Xamarin.Forms apps

Svg.Skia.Forms is an easy to use image control for displaying SVG still images
that can be used in Xamarin.Forms projects.

The library uses the [Svg.Skia](https://github.com/wieslawsoltes/Svg.Skia)
library for loading SVG images.

## Getting Started

In short, add the `Vividos.Svg.Skia.Forms` NuGet project to your Forms
project and all platform projects (Android, iOS, UWP).

Add the following namespace reference to every XAML page where you want to use
the `SvgImage` control:

    xmlns:ssf="clr-namespace:Svg.Skia.Forms;assembly=Svg.Skia.Forms"

Then use the `SvgImage` control, like this:

    <ssf:SvgImage Source="{Binding MyImage}"
        WidthRequest="48" HeightRequest="48" />

In the view model, you can provide the image source binding, like this:

    public ImageSource MyImage { get; }
       = ImageSource.FromResource("...");

## Screenshot

Here's a screenshot of the Samples app, running on Windows, showing various
ways to integrate `SvgImage` into a Forms app:

![Samples app](samples/samples-uwp.png)

## License

The library is licensed under the [MIT License](License.md).

The Toucan image used in the sample app is a free SVG image,
[designed by freepik.com](https://www.freepik.com).
