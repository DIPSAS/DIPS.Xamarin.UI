![dips.com](https://img.shields.io/badge/http%3A%2F%2Fdips.com-ENABLING%20EFFICIENT%20HEALTHCARE-red)


![Nuget](https://img.shields.io/nuget/v/dips.xamarin.ui?color=success&logoColor=white&logo=NuGet) ![GitHub last commit](https://img.shields.io/github/last-commit/Dipsas/DIPS.Xamarin.UI)

# DIPS.Xamarin.UI
![DIPS.Xamarin.UI_icon](https://raw.githubusercontent.com/DIPSAS/DIPS.Xamarin.UI/master/assets/DIPS_Xamarin_UI_128x128@slimmed.png)

## [Getting started](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Getting-Started)

## [Documentation](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki)

## [Releases](https://github.com/DIPSAS/DIPS.Xamarin.UI/releases) 

## Description

A shared UI library that DIPS use and maintain for their mobile applications. The library contains different components that can be used by any [Xamarin.Forms](https://github.com/xamarin/Xamarin.Forms) app. The visual parts of the library is created in collaboration with our internal UX team.

The library differentiate its components in three types:

### UI Components

Our UI components include different visual components that are well known to people using our apps. This can be a [sheet](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Sheet), [popup](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Popup), [radio buttons](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/RadioButton) with more. Our UI components focuses heavily on the [MVVM design pattern](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel). This means that you can find useful properties that are bindable and is easy to use with commands. Each UI component should have a well documented WIKI and a samples page that our consumers can look at.

### Resources

Our resources can be used in our pages and UI components. This can be [colors](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Colors), icons and [converters](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Converters) with more.

### API

Our API includes utilities that is useful when working with the MVVM pattern and XAML. This can be a [property changed api](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/The-API#propertychangedextensions|), [commands](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/The-API#asynccommand), [recursively searching the visual tree](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/The-API#getparentoftype) with more.

## Supported platforms

- [x] Android
- [x] iOS

This package do not support `UWP` or `WPF` because DIPS has a internal Windows desktop library with other components that are more suiting for desktop applications.

## Maintenance

The library gets constantly updated and new features gets added once we identify the need of having components shared between our apps. New features are added as a [issue](https://github.com/DIPSAS/DIPS.Xamarin.UI/issues) and are marked as a `[Spec]`.

We follow semantic versioning for our [nuget package](https://www.nuget.org/packages/DIPS.Xamarin.UI/).

## Contribution

The library will be maintained by DIPS AS, but the public is always welcome to contribute. Please see our [developer guidelines](https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Developer-guidelines) to get familiar with how to contribute.
