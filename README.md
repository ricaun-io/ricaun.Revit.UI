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

`ricaun.Revit.UI` package is design to work with the plugin [AppLoader](https://ricaun.com/AppLoader/) that allow loading Revit Applications on runtime.

```C#
[AppLoader]
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
                .SetToolTip("Esta � uma dica de ferramenta.")
                .SetLongDescription("Esta � uma descri��o.");
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

### RibbonPanel Extension
`RibbonPanelExtension` contains methods related to `Autodesk.Windows.RibbonPanel` and `Autodesk.Revit.UI.RibbonPanel`
```C#
UIControlledApplication application;
RibbonPanel ribbonPanel = application.CreatePanel("PanelName");
// application.CreatePanel("TabName", "PanelName");
// application.CreateOrSelectPanel("PanelName");
// application.CreateOrSelectPanel("TabName", "PanelName");
```
The method `GetRibbonItems` allow to select all `RibbonItem` concatenated on the `RibbonPanel`.
```C#
IList<RibbonItem> ribbonItems = ribbonPanel.GetRibbonItems();
```
The method `GetRibbonPanel` allow to select `Autodesk.Windows.RibbonPanel`.
```C#
Autodesk.Windows.RibbonPanel awRibbonPanel = ribbonPanel.GetRibbonPanel();
```
The method `Remove` allow to remove the `RibbonPanel` from `Autodesk.Windows` UI.
```C#
ribbonPanel.Remove();
```

### RibbonButton Extension
`RibbonButtonExtension` contains methods related to `PushButton` and `PushButtonData`
```C#
PushButton pushButton = ribbonPanel.CreatePushButton<IExternalCommand>();
// ribbonPanel.CreatePushButton<IExternalCommand>("ButtonName");
// ribbonPanel.CreatePushButton<IExternalCommand,IExternalCommandAvailability>();
// ribbonPanel.CreatePushButton<IExternalCommand,IExternalCommandAvailability>("ButtonName");
```
```C#
PushButtonData pushButtonData = ribbonPanel.NewPushButtonData<IExternalCommand>();
// ribbonPanel.NewPushButtonData<IExternalCommand>("ButtonDataName");
// ribbonPanel.NewPushButtonData<IExternalCommand,IExternalCommandAvailability>();
// ribbonPanel.NewPushButtonData<IExternalCommand,IExternalCommandAvailability>("ButtonDataName");
```

```C#
// pushButton.SetAvailability<IExternalCommandAvailability>();
// pushButtonData.SetAvailability<IExternalCommandAvailability>();
```

### RibbonItem Extension
`RibbonItemExtension` contains methods related to `RibbonItem`
```C#
var ribbonItem = ribbonPanel.CreatePushButton<Commands.Command>();
ribbonItem.SetText("RibbonItemName");
ribbonItem.SetToolTip("ToolTip");
ribbonItem.SetLongDescription("LongDescription");
ribbonItem.SetContextualHelp("ContextualHelpUrl");
ribbonItem.SetItemSize();
ribbonItem.SetShowText();
ribbonItem.SetShowImage();
ribbonItem.SetImage(ImageSource);
ribbonItem.SetLargeImage(ImageSource);
ribbonItem.SetToolTipImage(ImageSource);
```
or
```C#
var ribbonItem = ribbonPanel
    .CreatePushButton<Commands.Command>()
    .SetText("RibbonItemName")
    .SetToolTip("ToolTip")
    .SetLongDescription("LongDescription")
    .SetContextualHelp("ContextualHelpUrl")
    .SetItemSize()
    .SetShowText()
    .SetShowImage()
    .SetImage(ImageSource)
    .SetLargeImage(ImageSource)
    .SetToolTipImage(ImageSource);
```

### RibbonItemData Extension
`RibbonItemDataExtension` contains methods related to `RibbonItemData`
```C#
var ribbonItemData = ribbonPanel.NewPushButtonData<Commands.Command>();
ribbonItemData.SetText("RibbonItemName");
ribbonItemData.SetToolTip("ToolTip");
ribbonItemData.SetLongDescription("LongDescription");
ribbonItemData.SetContextualHelp("ContextualHelpUrl");
ribbonItemData.SetImage(ImageSource);
ribbonItemData.SetLargeImage(ImageSource);
ribbonItemData.SetToolTipImage(ImageSource);
```
or
```C#
var ribbonItemData = ribbonPanel
    .NewPushButtonData<Commands.Command>()
    .SetText("RibbonItemName")
    .SetToolTip("ToolTip")
    .SetLongDescription("LongDescription")
    .SetContextualHelp("ContextualHelpUrl")
    .SetImage(ImageSource)
    .SetLargeImage(ImageSource)
    .SetToolTipImage(ImageSource);
```

### RibbonComboBox Extension
`RibbonComboBoxExtension` contains methods related to `ComboBox`, `ComboBoxData`, and `ComboBoxMemberData`.
```C#
ComboBox comboBox = ribbonPanel.CreateComboBox();
// ribbonPanel.CreateComboBox("ComboBoxName");
// ribbonPanel.CreateComboBox("ComboBoxName", ComboBoxMemberData, ComboBoxMemberData, ...);
comboBox.AddComboBoxMembers(ComboBoxMemberData, ComboBoxMemberData, ...);
```
```C#
ComboBoxData comboBoxData = ribbonPanel.NewComboBoxData();
// ribbonPanel.NewComboBoxData("ComboBoxDataName");
```
```C#
ComboBoxMemberData comboBoxMemberData = ribbonPanel.NewComboBoxMemberData();
// ribbonPanel.NewComboBoxMemberData("ComboBoxMemberDataName");
comboBoxMemberData.SetGroupName("GroupName");
```

### RibbonPulldown Extension
`PulldownButtonExtension` contains methods related to `PulldownButton`
```C#
PulldownButton pulldownButton = ribbonPanel.CreatePulldownButton();
// ribbonPanel.CreatePulldownButton(PushButtonData, PushButtonData, ...);
// ribbonPanel.CreatePulldownButton("PulldownButtonName");
// ribbonPanel.CreatePulldownButton("PulldownButtonName", PushButtonData, PushButtonData, ...);
```
```C#
PushButton pushButton = pulldownButton.CreatePushButton<IExternalCommand>();
// pulldownButton.CreatePushButton<IExternalCommand>("ButtonName");
// pulldownButton.CreatePushButton<IExternalCommand,IExternalCommandAvailability>();
// pulldownButton.CreatePushButton<IExternalCommand,IExternalCommandAvailability>("ButtonName");
```
```C#
PushButtonData pushButtonData = pulldownButton.NewPushButtonData<IExternalCommand>();
// pulldownButton.NewPushButtonData<IExternalCommand>("ButtonDataName");
// pulldownButton.NewPushButtonData<IExternalCommand,IExternalCommandAvailability>();
// pulldownButton.NewPushButtonData<IExternalCommand,IExternalCommandAvailability>("ButtonDataName");
pulldownButton.AddPushButtons(PushButtonData, PushButtonData, ...);
```

### RibbonRadio Extension
`RibbonRadioExtension` contains methods related to `RadioButtonGroup`, `RadioButtonGroupData`, and `ToggleButtonData`
```C#
RadioButtonGroup radioButtonGroup = ribbonPanel.CreateRadioButtonGroup();
// ribbonPanel.CreateRadioButtonGroup("RadioButtonGroupName");
// ribbonPanel.CreateRadioButtonGroup("RadioButtonGroupName", ToggleButtonData, ToggleButtonData, ...);
radioButtonGroup.AddToggleButtons(ToggleButtonData, ToggleButtonData, ...);
```
```C#
RadioButtonGroupData radioButtonGroupData = ribbonPanel.NewRadioButtonGroupData("RadioButtonGroupDataName");
```
```C#
ToggleButtonData ToggleButtonData = ribbonPanel.NewToggleButtonData();
// ribbonPanel.NewToggleButtonData("ToggleButtonDataName");
// ribbonPanel.NewToggleButtonData<IExternalCommand>();
// ribbonPanel.NewToggleButtonData<IExternalCommand>("ToggleButtonDataName");
// ribbonPanel.NewToggleButtonData<IExternalCommand, IExternalCommandAvailability>();
// ribbonPanel.NewToggleButtonData<IExternalCommand, IExternalCommandAvailability>("ToggleButtonDataName");
```

### RibbonSplit Extension
`SplitButtonExtension` contains methods related to `SplitButton`
```C#
SplitButton splitButton = ribbonPanel.CreateSplitButton();
// ribbonPanel.CreateSplitButton(PushButtonData, PushButtonData, ...);
// ribbonPanel.CreateSplitButton("SplitButtonName");
// ribbonPanel.CreateSplitButton("SplitButtonName", PushButtonData, PushButtonData, ...);
```
```C#
PushButton pushButton = splitButton.CreatePushButton<IExternalCommand>();
// splitButton.CreatePushButton<IExternalCommand>("ButtonName");
// splitButton.CreatePushButton<IExternalCommand,IExternalCommandAvailability>();
// splitButton.CreatePushButton<IExternalCommand,IExternalCommandAvailability>("ButtonName");
```
```C#
PushButtonData pushButtonData = splitButton.NewPushButtonData<IExternalCommand>();
// splitButton.NewPushButtonData<IExternalCommand>("ButtonDataName");
// splitButton.NewPushButtonData<IExternalCommand,IExternalCommandAvailability>();
// splitButton.NewPushButtonData<IExternalCommand,IExternalCommandAvailability>("ButtonDataName");
splitButton.AddPushButtons(PushButtonData, PushButtonData, ...);
```

### RibbonTextBox Extension
`RibbonTextBoxExtension` contains methods related to `TextBox` and `TextBoxData`
```C#
TextBox textBox = ribbonPanel.CreateTextBox();
// ribbonPanel.CreateTextBox("TextBoxName");
textBox.SetValue("Value");
textBox.SetPromptText("PromptText");
textBox.SetShowImageAsButton(true);
textBox.SetSelectTextOnFocus(true);
textBox.SetWidth(120);
textBox.AddEnterPressed(OnEnterPressed);
textBox.RemoveEnterPressed(OnEnterPressed);
```
```C#
TextBoxData textBoxData = ribbonPanel.NewTextBoxData();
// ribbonPanel.NewTextBoxData("TextBoxNameData");
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

### RibbonTab Extension
`RibbonTabExtension` contains methods related to `Autodesk.Windows.RibbonTab`
```C#
Autodesk.Windows.RibbonTab awRibbonTab = ribbonPanel.GetRibbonTab();
Autodesk.Windows.RibbonTab awRibbonTab = RibbonTabExtension.GetRibbonTab("TabId");
IList<Autodesk.Windows.RibbonTab> awRibbonTabs = RibbonTabExtension.GetRibbonTabs();
```
The method `SetPanelsOrderBy` and `SetPanelsOrderByTitle` allow reorder the `RibbonPanel` in the `RibbonTab` UI.
```C#
Autodesk.Windows.RibbonTab awRibbonTab = ribbonPanel.GetRibbonTab();
awRibbonTab.SetPanelsOrderBy(e => e.Source.Title);
awRibbonTab.SetPanelsOrderByTitle();
```
The method `Remove` allow to remove the `RibbonTab` from `Autodesk.Windows` UI.
```C#
awRibbonTab.Remove();
```

### QuickAccessToolBar Extension
`QuickAccessToolBarExtension` contains methods related to add and remove `Autodesk.Windows.RibbonItem` to the QuickAccessToolBar.
```C#
ribbonItem.AddQuickAccessToolBar();
```
```C#
ribbonItem.RemoveQuickAccessToolBar();
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

### Ribbon Description Extension
... Todo

## Release

* [Latest release](../../releases/latest)

## License

This Project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

Credit to [icons8.com](https://icons8.com/) for the icons on the Example.

---

Do you like this package? Please [star this project on GitHub](../../stargazers)!