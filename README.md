# Svg.Skia.Forms - SVG images in Xamarin.Forms apps

Svg.Skia.Forms is an easy to use image control for displaying SVG still images
that can be used in Xamarin.Forms projects.

The library uses the [Svg.Skia](https://github.com/wieslawsoltes/Svg.Skia)
library for loading SVG images.

## Installation

* Add this NuGet package:
[![NuGet](https://img.shields.io/nuget/vpre/Vividos.Svg.Skia.Forms.svg?label=NuGet)](https://www.nuget.org/packages/Vividos.Svg.Skia.Forms)

## Getting Started

Add the following namespace reference to every XAML page where you want to use
the `SvgImage` control:

    xmlns:ssf="clr-namespace:Svg.Skia.Forms;assembly=Svg.Skia.Forms"

Then use the `SvgImage` control, like this:

    <ssf:SvgImage Source="{Binding MyImage}"
        WidthRequest="48" HeightRequest="48" />

In the view model, you can provide the image source binding, like this:

    public ImageSource MyImage { get; }
       = ImageSource.FromResource("...");

See also the more detailed [Documentation](Documentation.md).

## Build Status

Builds are created using GitHub Actions:
[![GitHub](https://github.com/vividos/Svg.Skia.Forms/actions/workflows/net-build.yml/badge.svg)](https://github.com/vividos/Svg.Skia.Forms/actions/workflows/net-build.yml)

Coverage is checked using Coveralls:
[![Coverage Status](https://coveralls.io/repos/github/vividos/Svg.Skia.Forms/badge.svg?branch=main)](https://coveralls.io/github/vividos/Svg.Skia.Forms?branch=main)

## Screenshot

Here's a screenshot of the Samples app, running on Windows, showing various
ways to integrate `SvgImage` into a Forms app:

![Samples app](samples/samples-uwp.png)

## License

The library is licensed under the [MIT License](License.md).

The Toucan and Colibri images used in the sample app are free SVG images,
[designed by freepik.com](https://www.freepik.com).
