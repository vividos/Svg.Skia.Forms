# Svg.Skia.Forms Documentation

This is the official documentation for the Svg.Skia.Forms library.

## Installation

[![NuGet](https://img.shields.io/nuget/vpre/Vividos.Svg.Skia.Forms.svg?label=NuGet)](https://www.nuget.org/packages/Vividos.Svg.Skia.Forms)

`Svg.Skia.Forms` is available via NuGet, and can be installed using the
"Manage NuGet Packages..." menu item for projects. Search for the package
`Vividos.Svg.Skia.Forms` and add it to your Xamarin.Forms project. It's not
necessary to add it to the Android, iOS or UWP platform projects.

You can also install the package using the `Package Manager Console`:

    Install-Package Vividos.Svg.Skia.Forms

Note that the library introduces some external dependencies to other NuGet
packages and libraries, among them:

- [SkiaSharp](https://github.com/mono/SkiaSharp)
- [Svg.Skia](https://github.com/wieslawsoltes/Svg.Skia)
- [Fizzler](https://github.com/atifaziz/Fizzler)

Check out the license of the packages if they fit your project.

When testing a new feature or a bug fix, the
[GitHub Actions based build](https://github.com/vividos/Svg.Skia.Forms/actions/workflows/net-build.yml)
also produces NuGet packages that can be downloaded in the "Artifacts"
section. These packages can be referenced for testing by using a
`NuGet.config` file and using `packageSources` to point to a private folder:
https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file#package-source-sections
Package sources can also be specified in the Visual Studio Options dialog.

## Versioning

The library and NuGet package uses
[Semantic Versioning 2.0.0](https://semver.org/) to version the created
assemblies. The version number will look like this:

    MAJOR.MINOR.PATCH

The `MAJOR` version only changes on incompatible API changes. The `MINOR`
version changes when new features are added, but are backward compatible. The
`PATCH` version will increase for backward compatible bug fixes.

Normally you're safe when updating to every minor version with the same major
version, but you may have to do code changes when switching major versions.

For preview NuGet packages and assemblies built using GitHub Actions, the
version may look like this:

    MAJOR.MINOR.PATCH-preview.X.Y

Here, `X` specifies the number of preview version, starting with 1, and `Y` is
the number of commits since the latest preview.

Note that the `Informational Version` of the actual assembly also contains the
Git repository SHA1 hash value that was used for building the assembly.

## Usage

The library provides a single `Xamarin.Forms` control based on
`Xamarin.Forms.View` (actually, a `SkiaSharp.Views.Forms.SKCanvasView`).
All properties that these base control classes provided are supported by the
control.

### XAML

When using XAML to describe pages, you have to first introduce the namespace
to that page, by adding the following to the page's root xml node:

    <ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
        xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
        xmlns:ssf="clr-namespace:Svg.Skia.Forms;assembly=Svg.Skia.Forms"
        x:Class="Svg.Skia.Forms.Sample.SamplePage">

With the `xmlns:ssf` attribute specified on the page, the `SvgImage` control
that lives in that namespace can be used:

    <ssf:SvgImage Source="..."
                  WidthRequest="48"
                  HeightRequest="48" />

Note that you have to specify a `WidthRequest` and `HeightRequest` attribute,
otherwise the control has its default size. You can use other attributes like
`HorizontalOptions` and `VerticalOptions to position the control.

### C#

When using C# code to describe the page's content, create the `SvgImage`
control like this:

    var image = new SvgImage
    {
        Source = ImageSource.FromResource(...),
        WidthRequest = 64,
        HeightRequest = 64,
    });

### Image source

Image sources can be specified using `string` values or `ImageSource`
instances. Internally, the `string` values are also converted to an
`ImageSource`. SVG images can be stored in the `Xamarin.Forms` project, in any
other .NET Standard assembly or in the platform projects.

Just like with every other bindable property, you can use `{Binding}` to bind
to a `string` or `ImageSource` property in your view model:

    <ssf:SvgImage Source="{Binding AppIcon}"

or

    var image = new SvgImage
    {
        Source = new Binding("AppIcon"),

The default binding mode is `OneWay`.

#### Resource URI

You can specify a `resource://` URI to reference images, like this:

    resource://ProjectName.Path.To.Image.svg
    resource://ProjectName.Path.To.Image.svg?assembly=AssemblyName

The `ProjectName` part corresponds to the project's name where the SVG image
is placed. `Path.To.Image.svg` is derived from the path inside the project,
where path slashes are replaced with dots. The path `Assets/colibri.svg` in
the project `Svg.Skia.Forms.Sample` becomes
`Svg.Skia.Forms.Sample.Assets.colibri.svg`.

Examples:

    resource://Svg.Skia.Forms.Sample.Assets.colibri.svg
    resource://Svg.Skia.Forms.Sample.Assets.colibri.svg?assembly=Svg.Skia.Forms.Sample

When the assembly isn't specified, the control tries to find the image in the
calling assembly.

Internally, the resource URI is converted to an `UriImageSource` using
`ImageSource.FromResource()`. So you can also specify the source like this:

    Source = ImageSource.FromResource(
        "Svg.Skia.Forms.Sample.Assets.colibri.svg",
        typeof(SamplePage).Assembly);

Add the SVG images to the project and be sure to set the `Build Action` in the
file's properties as `Embedded Resource`. In the `.csproj` file, the image is
added using a `<EmbeddedResource>` tag.

#### Data URI 

You can also specify the image to display using a `data:` URI containing an
SVG image. This is useful if you're generating the SVG in code, or you have
another source for the SVG image.

These two variants can be used:

    data:image/svg+xml,SvgImageText
    data:image/svg+xml;base64,Base64EncodedSvgImageText

Examples:

    data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="64" height="64"><rect x="16" y="16" width="32" height="32" /></svg>
    data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI2NCIgaGVpZ2h0PSI2NCI+PHJlY3QgeD0iMTYiIHk9IjE2IiB3aWR0aD0iMzIiIGhlaWdodD0iMzIiIC8+PC9zdmc+

The usage of `data:` URIs is borrowed from HTML, where this approach also
works for specifying inline images.

#### File Path

You can also speficy the path to an image using a file path, just like when
referencing platform-specific images in Xamarin.Forms' `Image` control:

    <ssf:SvgImage Source="Assets/colibri.svg"

When using C#, use the following method to create the `ImageSource`:

    Source = ImageSource.FromFile("Assets/colibri.svg")

The images must be stored in the platform projects, using the following build
actions:

|Platform|Build Action
|---     |---
|Android |AndroidResource
|iOS     |BundleResource
|UWP     |Content / Do not copy

See this chapter in the Microsoft Docs on how to integrate images for the
`Image` control. The same infos apply for the `SvgImage` control:
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/images?tabs=windows#local-images

#### Other URI types

Currently no other URI types, like `https` or `file` are supported.

## Sample app

The Git repository comes with a sample project `Svg.Skia.Forms.Sample` that
contains a [Xamarin.Forms](https://github.com/xamarin/Xamarin.Forms) app that
targets Android, iOS and UWP. The
[`SamplePage`](https://github.com/vividos/Svg.Skia.Forms/blob/main/samples/Svg.Skia.Forms.Sample/SamplePage.xaml)
shows various ways to add `SvgImage` controls to a page. The Binding
Properties defined in the
[`SamplePageViewModel`](https://github.com/vividos/Svg.Skia.Forms/blob/main/samples/Svg.Skia.Forms.Sample/SamplePageViewModel.cs)
are used to show how binding works.

## Credits

The library uses the [SkiaSharp](https://github.com/mono/SkiaSharp)
library to draw SVG images. The library is licensed using the
[MIT License](https://github.com/mono/SkiaSharp/blob/master/LICENSE.md).

The library uses the [Svg.Skia](https://github.com/wieslawsoltes/Svg.Skia)
library to load SVG images. The library is licensed using the
[MIT License](https://github.com/wieslawsoltes/Svg.Skia/blob/master/LICENSE.TXT).

The library uses [MinVer](https://github.com/adamralph/minver) for versioning.
The tool is licensed using the
[Apache License 2.0](https://github.com/adamralph/minver/blob/main/LICENSE).

The cog-outline image used in the sample app is from the Google Material
Icons; the icons are licensed under the
[Apache License 2.0](https://github.com/google/material-design-icons/blob/master/LICENSE).

The Toucan and Colibri images used in the sample app are free SVG images,
[designed by freepik.com](https://www.freepik.com). They are availabe in .eps
format, here:
https://www.freepik.com/free-vector/bird-flat-color-icons-set_4265855.htm
