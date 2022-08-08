# ricaun.Revit.UI

[![Revit 2017](https://img.shields.io/badge/Revit-2017+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Publish](../../actions/workflows/Publish.yml/badge.svg)](../../actions)
[![Develop](../../actions/workflows/Develop.yml/badge.svg)](../../actions)
[![Release](https://img.shields.io/nuget/v/ricaun.Revit.UI?logo=nuget&label=release&color=blue)](https://www.nuget.org/packages/ricaun.Revit.UI)

## Features

### Autodesk Extension
The `SetAutodeskOwner()` method applies the `window` as an Autodesk owner.
```C#
Window window = new MainWindow();
window.SetAutodeskOwner();
```

### Bitmap Extension
The `GetBitmapSource()` method transform `Bitmap`, `Icon`, `Image`, `base64orUriString` in `BitmapSource`.
```C#
System.Drawing.Bitmap bitmap;
BitmapSource bitmapSource = bitmap.GetBitmapSource();
```
```C#
System.Drawing.Icon icon;
BitmapSource bitmapSource = icon.GetBitmapSource();
```
```C#
System.Drawing.Image image;
BitmapSource bitmapSource = image.GetBitmapSource();
```
```C#
string base64orUri;
BitmapSource bitmapSource = base64orUri.GetBitmapSource();
```

### Language Extension
The `LanguageExtension` contains methods related to the application language.
```C#
LanguageType languageType = LanguageExtension.GetLanguageType();
```
```C#
LanguageExtension.IsEnglish;
LanguageExtension.IsSpanish;
LanguageExtension.IsRussian;
LanguageExtension.IsHungarian;
LanguageExtension.IsDutch;
LanguageExtension.IsFrench;
LanguageExtension.IsGerman;
LanguageExtension.IsSpanish;
LanguageExtension.IsRussian;
LanguageExtension.IsPolish;
LanguageExtension.IsKorean;
LanguageExtension.IsCzech;
LanguageExtension.IsChineseSimplified;
LanguageExtension.IsChineseTraditional;
LanguageExtension.IsBrazilianPortuguese;
```

### QuickAccessToolBar Extension
The `QuickAccessToolBarExtension` contains methods related to add and remove `Autodesk.Windows.RibbonItem` to the QuickAccessToolBar.
```C#
ribbonItem.AddQuickAccessToolBar();
```
```C#
ribbonItem.RemoveQuickAccessToolBar();
```

### RibbonButton Extension
### RibbonComboBox Extension
### Ribbon Description Extension
### Ribbon Help Extension
### RibbonItemData Extension
### RibbonItem Extension
### RibbonPanel Extension
### RibbonPulldown Extension
### RibbonRadio Extension
### RibbonSafe Extension
### RibbonSplit Extension
### RibbonTab Extension
### RibbonTextBox Extension
### RibbonUtil Extension

## Release

* [Latest release](../../releases/latest)

## License

This Project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

Credit to [icons8.com](https://icons8.com/) for the icons on the Example.

---

Do you like this package? Please [star this project on GitHub](../../stargazers)!