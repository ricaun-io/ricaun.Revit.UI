# ricaun.Revit.UI

`ricaun.Revit.UI` package makes it easier to work with UI and RevitApi, especially with RibbonPanel, RibbonTab, RibbonButton, and RibbonItem in general.

[![Revit 2017](https://img.shields.io/badge/Revit-2017+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Publish](../../actions/workflows/Publish.yml/badge.svg)](../../actions)
[![Develop](../../actions/workflows/Develop.yml/badge.svg)](../../actions)
[![Release](https://img.shields.io/nuget/v/ricaun.Revit.UI?logo=nuget&label=release&color=blue)](https://www.nuget.org/packages/ricaun.Revit.UI)

## Features

`ricaun.Revit.UI` package is design to work with the plugin [ApplicationLoader](https://ricaun.com/ApplicationLoader/) that allow loading Revit Applications on runtime.

```C#
[ApplicationLoader]
public class App : IExternalApplication
{
    private static RibbonPanel ribbonPanel;
    public Result OnStartup(UIControlledApplication application)
    {
        ribbonPanel = application.CreatePanel("PanelName");

        var commandButton = ribbonPanel.CreatePushButton<Commands.Command>()
            .SetText("Command")
            .SetToolTip("This is a tooltip.")
            .SetLongDescription("This is a description.")
            .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

        if (LanguageExtension.IsBrazilianPortuguese)
        {
            commandButton.SetText("Comando")
                .SetToolTip("Esta é uma dica de ferramenta.")
                .SetLongDescription("Esta é uma descrição.");
        }

        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        ribbonPanel?.Remove();
        return Result.Succeeded;
    }
}
```

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
`LanguageExtension` contains methods related to the application language.
```C#
LanguageType languageType = LanguageExtension.GetLanguageType();
// LanguageExtension.IsEnglish;
// LanguageExtension.IsSpanish;
// LanguageExtension.IsRussian;
// LanguageExtension.IsHungarian;
// LanguageExtension.IsDutch;
// LanguageExtension.IsFrench;
// LanguageExtension.IsGerman;
// LanguageExtension.IsSpanish;
// LanguageExtension.IsRussian;
// LanguageExtension.IsPolish;
// LanguageExtension.IsKorean;
// LanguageExtension.IsCzech;
// LanguageExtension.IsChineseSimplified;
// LanguageExtension.IsChineseTraditional;
// LanguageExtension.IsBrazilianPortuguese;
```

### QuickAccessToolBar Extension
`QuickAccessToolBarExtension` contains methods related to add and remove `Autodesk.Windows.RibbonItem` to the QuickAccessToolBar.
```C#
ribbonItem.AddQuickAccessToolBar();
```
```C#
ribbonItem.RemoveQuickAccessToolBar();
```

### RibbonButton Extension
`RibbonButtonExtension` contains methods related to `PushButton` and `PushButtonData`
```C#
PushButton pushButton = ribbonPanel.CreatePushButton<IExternalCommand>();
// PushButton pushButton = ribbonPanel.CreatePushButton<IExternalCommand>("ButtonName");
// PushButton pushButton = ribbonPanel.CreatePushButton<IExternalCommand,IExternalCommandAvailability>();
// PushButton pushButton = ribbonPanel.CreatePushButton<IExternalCommand,IExternalCommandAvailability>("ButtonName");
```
```C#
PushButtonData pushButtonData = ribbonPanel.NewPushButtonData<IExternalCommand>();
// PushButtonData pushButtonData = ribbonPanel.NewPushButtonData<IExternalCommand>("ButtonDataName");
// PushButtonData pushButtonData = ribbonPanel.NewPushButtonData<IExternalCommand,IExternalCommandAvailability>();
// PushButtonData pushButtonData = ribbonPanel.NewPushButtonData<IExternalCommand,IExternalCommandAvailability>("ButtonDataName");
```

### RibbonComboBox Extension
`RibbonComboBoxExtension` contains methods related to `ComboBox`, `ComboBoxData`, and `ComboBoxMemberData`.
```C#
ComboBox comboBox = ribbonPanel.CreateComboBox("ComboBoxName");
// ComboBox comboBox = ribbonPanel.CreateComboBox("ComboBoxName", ComboBoxMemberData, ComboBoxMemberData, ...);
// comboBox.AddItems(ComboBoxMemberData, ComboBoxMemberData, ...);
```
```C#
ComboBoxData comboBoxData = ribbonPanel.NewComboBoxData("ComboBoxDataName");
```
```C#
ComboBoxMemberData comboBoxMemberData = ribbonPanel.NewComboBoxMemberData("ComboBoxMemberDataName");
comboBoxMemberData.SetGroupName("GroupName");
```

### Ribbon Description Extension
### RibbonItemData Extension
### RibbonItem Extension
### RibbonPanel Extension
### RibbonPulldown Extension
`PulldownButtonExtension` contains methods related to `PulldownButton`
```C#
PulldownButton pulldownButton = ribbonPanel.CreatePulldownButton();
// PulldownButton pulldownButton = ribbonPanel.CreatePulldownButton(PushButtonData, PushButtonData, ...);
// PulldownButton pulldownButton = ribbonPanel.CreatePulldownButton("PulldownButtonName");
// PulldownButton pulldownButton = ribbonPanel.CreatePulldownButton("PulldownButtonName", PushButtonData, PushButtonData, ...);
```

### RibbonRadio Extension
`RibbonRadioExtension` contains methods related to `RadioButtonGroup`, `RadioButtonGroupData`, and `ToggleButtonData`
```C#
RadioButtonGroup radioButtonGroup = ribbonPanel.CreateRadioButtonGroup("RadioButtonGroupName");
// RadioButtonGroup radioButtonGroup = ribbonPanel.CreateRadioButtonGroup("RadioButtonGroupName", ToggleButtonData, ToggleButtonData, ...);
// radioButtonGroup.AddItems(ToggleButtonData, ToggleButtonData, ...);
```
```C#
RadioButtonGroupData radioButtonGroupData = ribbonPanel.NewRadioButtonGroupData("RadioButtonGroupDataName");
```
```C#
ToggleButtonData ToggleButtonData = ribbonPanel.NewToggleButtonData("ToggleButtonDataName");
// ToggleButtonData ToggleButtonData = ribbonPanel.NewToggleButtonData<IExternalCommand>();
// ToggleButtonData ToggleButtonData = ribbonPanel.NewToggleButtonData<IExternalCommand>("ToggleButtonDataName");
// ToggleButtonData ToggleButtonData = ribbonPanel.NewToggleButtonData<IExternalCommand, IExternalCommandAvailability>();
// ToggleButtonData ToggleButtonData = ribbonPanel.NewToggleButtonData<IExternalCommand, IExternalCommandAvailability>("ToggleButtonDataName");
```

### RibbonSplit Extension
`SplitButtonExtension` contains methods related to `SplitButton`
```C#
SplitButton splitButton = ribbonPanel.CreateSplitButton();
//SplitButton splitButton = ribbonPanel.CreateSplitButton(PushButtonData, PushButtonData, ...);
//SplitButton splitButton = ribbonPanel.CreateSplitButton("SplitButtonName");
//SplitButton splitButton = ribbonPanel.CreateSplitButton("SplitButtonName", PushButtonData, PushButtonData, ...);
```

### RibbonTab Extension
### RibbonTextBox Extension
`RibbonTextBoxExtension` contains methods related to `TextBox` and `TextBoxData`
```C#
TextBox textBox = ribbonPanel.CreateTextBox("TextBoxName");
textBox.SetValue("Value");
textBox.SetPromptText("PromptText");
textBox.SetShowImageAsButton(true);
textBox.SetSelectTextOnFocus(true);
```
```C#
TextBoxData textBoxData = ribbonPanel.NewTextBoxData("TextBoxNameData");
```

### RibbonUtil Extension
`Autodesk.Revit.UI.RibbonItem` --> `Autodesk.Windows.RibbonItem`
```C#
Autodesk.Revit.UI.RibbonItem ribbonItem;
Autodesk.Windows.RibbonItem awRibbonItem = ribbonItem.GetRibbonItem();
```
`Autodesk.Revit.UI.RibbonPanel` --> `Autodesk.Windows.RibbonPanel`
```C#
Autodesk.Revit.UI.RibbonPanel ribbonPanel;
Autodesk.Windows.RibbonPanel awRibbonPanel = ribbonPanel.GetRibbonPanel();
```


## Release

* [Latest release](../../releases/latest)

## License

This Project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

Credit to [icons8.com](https://icons8.com/) for the icons on the Example.

---

Do you like this package? Please [star this project on GitHub](../../stargazers)!