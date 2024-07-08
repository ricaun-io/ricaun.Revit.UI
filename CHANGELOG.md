# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.6.3] / 2024-07-06
### Features
- `RibbonThemeUtils` to change the theme of the Ribbon. (Revit 2019+)
- Set project configuration to support `net47` and `net48`.
### Updated
- Change `GetRibbonItem` to `GetRibbonItem_Alternative` to fix null when panel is removed.
- Update `SetImage` to work with `ComboBoxMember`
- Add `CreateComboBoxMember` to create `ComboBoxMember`.
- Add `RibbonThemeImageUtils` to change theme for `ImageSource`.
- Update `RibbonThemeImageUtils` with public `GetThemeImageSource`.
- Add `RibbonThemePanelUtils` to update the theme for itens in the `RibbonPanel`.
- Update `RibbonPanel` create and remove to update the theme of the itens.
### Tests
- Add `RibbonThemeUtilsTests` to test the theme change event.
- Add `ComboBoxMember` tests for `Image`, `Group` and `Current`.
### Example
- Add `AppTheme` to test theme change features for `RibbonItem`. 

## [0.6.2] / 2024-01-09 - 2024-02-05
### Features
- `SetListImageSize` to change the size of the image in the `PulldownButton` and `SplitButton`.
### Removed
- Remove support `net7.0-windows`.
### Tests
- Add Test to `SetListImageSize` in `PulldownButton`.

## [0.6.1] / 2023-12-08 - 2023-12-18
### Features
- `SetLargeImage` always set small image when `ico`.
### Updated
- Update `SetLargeImage` to change small image when `BitmapFrame`.
- Update `RibbonDescription` images to `object` to support `string`.
### Tests
- Add Test to `ReSetImage` when `ico`.

## [0.6.0] / 2023-11-19 - 2023-11-29
### Features
- Update to support `net7.0-windows` and `net8.0-windows`
- SetImage works with `Resources` without assembly name.
- Change `System.Drawing` to a separate namespace and class.
- Icons `GetBitmapSource` return the biggest frame.
### Updated
- Update `Build` project
- Update `Example` project
- Fix `MovePanelTo` remove panel from `RibbonTabsDictionary`
- Add `StackTraceUtils` to find the caller assembly.
- Update `BitmapExtension` to enable `Resources` without assembly name.
### Tests
- Test `MovePanelTo_Modify_CreatePanel_SameName`
- Test `StackTraceUtils`
- Test `Resources` without assembly name.
- Test `Drawing` Resources.
- Test `ResourceTests`, `ResourcePngTests` and `ResourcesFramesTests`

## [0.5.7] / 2023-11-17
### Features
- LargeImage changes `TextBox` Image
- Create `RowLargeStackedItems` with large image and two itens
### Updated
- Update Example `Icons` to get random icon using `UIFrameworkRes`
### Tests
- Test `ComboBox` and `TextBox` Image/LargeImage
- Test `RowLargeStackedItems` with large image and two itens

## [0.5.6] / 2023-11-08
### Features
- Update Remove and QuickAccess
### Updated
- Update `QuickAccessToolBarExtension` with internal methods.
- Update `MoveToRibbonTab` to move to `Modify` tab.
### Tests
- Add Tests for `QuickAccessToolBarExtension`
- Add Tests for `RibbonHelp` and `RibbonItem`
- Add Tests for `AppLoader`
- Add Tests for `MoveToRibbonTab`

## [0.5.5] / 2023-11-03
### Features
- Language Update and Test

## [0.5.4] / 2023-10-19
### Features
- Add `ComboBox` methods extension
### Updated
- Update `RibbonItemPanelExtension` and remove `Clone`.
### ComboBox
- Add SetWidth
- Add SetCurrent
- Add AddCurrentChanged
- Add RemoveCurrentChanged
- Add AddDropDownOpened
- Add RemoveDropDownOpened
- Add AddDropDownClosed
- Add RemoveDropDownClosed
### Fixed
- Clone `RibbonItem` not clone events and break `TextBox`.
### Tests
- Add `RevitTextBoxTests`
- Add `RevitComboBoxTests`

## [0.5.3] / 2023-07-13
## Updated
- Update Image/LargeImage extension code.
- Update `UpdateRibbonDescription` Image code.
### UI
- Add `RibbonDialogLauncherExtension`
### Tests
- Add `RevitDialogLauncherTests`
- Add `RevitStackedItemsTests` for `RowStackedItems` and `FlowStackedItems`
- Add `RevitCreateItemsExtensionTests`
- Add `RevitNewItemsExtensionTests`
- Add `RevitCreateItemsImageTests`
- Add `RevitNewItemsImageTests`

## [0.5.2] / 2023-04-17
### UI
- Update `NewPushButtonData` the default `Text` equals to `targetName`.
### Tests
- Add `RevitCreateItemsCommandTests`
- Add `RevitCreateItemsCommandSetTests`
- Update `RevitCreateItemsWithNameTests`

## [0.5.1] / 2023-04-14
### RibbonStackExtension
- Add RowStackedItems
- Add FlowStackedItems
### TextBox
- Add SetWidth
- Add AddEnterPressed
- Add RemoveEnterPressed
### ComboBox
- Update AddComboBoxMembers
### Updated
- Update Name in creation of Item with `IsNullOrWhiteSpace`
- Remove CreateCopy - Use Clone insted
### Tests
- Add RevitCreateItemsTests
- Add RevitCreateItemsWithNameTests
- Add RevitGetRibbonItemsTests

## [0.5.0] / 2023-04-08
### Features
- RibbonItem - CreateCopy
- RibbonPanel - MoveToRibbonTab
- RibbonSplit - CreatePushButton, NewPushButtonData
- RibbonPulldown - CreatePushButton, NewPushButtonData
### Utils
- RibbonTabUtils - CreateRibbonPanel, RemoveRibbonPanel
- RibbonModifyUtils - CreateRibbonPanel, RemoveRibbonPanel
### Updated
- Remove Obsolete AddPushButton/Close
- Update RibbonButtonExtension - Add SetAvailability
- Update RibbonSafeExtension - Base CreateButton/NewButton
- Update all Ribbons with GenerateSafeButtonName
- Update RibbonRadio - AddItems to AddToggleButtons
- Update NewPushButtonData<T> to work with PushButtonData and ToggleButtonData
- Update Obsolete ConsoleAttribute
- Update Remove RibbonPanel from internal RibbonTabsDictionary
- Update CreatePanel uses Add-Ins
- Update `AddPushButtons` with <T>
### Tests
- Add `IsTestProject` in csproj
- Add Build LocalTest
- Add RevitTests
- Add Panels.RevitPanelTests
- Add Panels.RevitTabPanelTests

## [0.4.0] / 2022-08-23
### Features
- Bitmap to BitmapSource perfect pixel

## [0.3.2] / 2022-08-16
### Features
- Add `AppLoader` Attribute

## [0.3.1] / 2022-08-09
### Features
- Add `SetPanelsOrderByTitle` and `SetPanelsOrderBy`
### Added
- Add internal `ObservableCollectionExtension`
### Updated
- Update `MoveRibbonPanel`
- Update Order methods
- Update methods to `internal`

## [0.3.0] / 2022-08-08
### Features
- Add `IExternalCommandAvailability` on `NewToggleButtonData` Command
### Updated
- Update methods to `internal` and `private`

## [0.2.1] / 2022-08-08
### Updated
- Update `SetAutodeskOwner`
### Added
- Add `ApplicationLoader`

## [0.2.0] / 2022-07-09
### Added
- Add `CreatePushButton` and Obsolete `AddPushButton`
### Updated
- Update `RibbonSettings` Add with Action
- Update `RibbonDescription` (LanguageType) 

## [0.1.2] / 2022-05-17
### Added
- Add `IExternalCommandAvailability` on `NewPushButtonData` Command

## [0.1.1] / 2022-05-07
### Features
- IsLanguageExtension for each Language
- Remove RibbonTab name from Revit Dictionary - `GetRibbonTabsDictionary`
### Added
- Add `Autodesk.Windows.ComponentManager.IsApplicationFrameEnabled` to disable buttons on modeless Example

## [0.1.0] / 2022-04-04
### Added
- Add `revit.ico` to Test
### Fixed
- Add `pack://application:,,,` to try to fix the `GetBitmapSource`

## [0.0.9] / 2022-03-23
### Fixed
- Update `PackageBuilder` to fix Pack not Sign
### Added
- Add CreateOrSelectPanel
### 2022-02-14
- Update AutodeskExtension - Add IsApplicationActive 
- Update Build
- Clear App

## [0.0.8] / 2022-02-14
- Add RibbonTextBoxExtension
- Add RibbonRadioExtension
- Move to Button/Pulldown/Split/ComboBox Extensions
- Move VerifyNameExclusive to RibbonSafeExtension
- Update RelayCommand
- 2022-02-12
- Example Add RelayCommand and Test Some Things to Detect Revit Busy
- Example Add View/Model/Test
- Update Example - Fody/PropertyChanged
- 2022-02-09
- Add ComboBox Methods
- Add ComboBox Items on `GetRibbonItems`
- Add ComboBox
- Fix MoveRibbonPanel - Add Max Move / Add Negative

## [0.0.7] / 2022-02-08
- Add Availability class
- Update xml documetation
- Fix AutoSelect base64 or uri
- Add string on SetImage / SetLargeImage
- 2022-02-07
- Add QuickAccessToolBar on Autodesk.Revit.UI.RibbonItem
- Add Pack UIFrameworkRes Reference
- Remove RibbonItem from RibbonPanel - Include Split/Down/Stacked
- Test Online Icons Scale
- Test Scale
- Test IsDownloading Icon
- Icon Select Frame
- Teste PushButtonData Set Proprieties
- Change CreatePulldownButton to params
- Change CreateSplitButton to params
- Add RibbonHelpExtension
- Add RibbonItemDataExtension
- Clear Code - Remove not used stuff
- Add RibbonUtilExtension
- Add RibbonTabExtension
- TODO: Add PushButtonData, RibbonItemData SetImage / Description / Othes : DONE
- TODO: Add pack component BitmapSource : DONE
- TODO: Add Icon selector size with BitmapDecoder : DONE
- TODO: Add Remove RibbonItem From RibbonPanel : DONE
- 2022-02-03
- Add Remove RibbonItem on the Panel

## [0.0.6] / 2022-02-02
- Special Icons8 class
- Update Example Version
- Add Icon / InstallationFiles
- Add More Icons
- Update Readme
- Update `GetBitmapSource` base64orUri
- Add Icons8 on Example
- Change SetImage and SetLargeImage to RibbonItem
- Fix RemoveQuickAccessToolBar on `Remove` RibbonPanel
- Add GetName `Type`
- Add SetShowImage
- Add Action on `RibbonDescription`
- Add TRibbonButton on `RibbonDescriptionExtension`
- Add TRibbonItem on `RibbonDescriptionExtension`
- 2022-01-31
- Remove QuickAccessToolBar when Remove RibbonPanel
- Add QuickAccessToolBarExtension
- 2022-01-25
- Add GetRibbonTabs

## [0.0.5] / 2022-01-24
- Add SetImage / SetLongDescription
- Add UpdateRibbonDescription
- Add RibbonDescription
-- [0.0.5] / 2022-01-20
- Add Move RibbonPanelTo
- Add Remove RibbonTab
- Add `GetRibbonPanel` using `m_RibbonPanel`
- Remove Panel
- Add OrderPanels
- Clear Code
- Change to Now.Tick
- Add ConsoleAttribute
- Add RibbonItem Set
- Add RibbonDescriptionExtension

## [0.0.4] / 2022-01-12
- Add NugetApiUrl & NugetApiKey
- Update Readme
- Update Build Project

## [0.0.3] / 2021-12-21
- Add TickNumber
- Remove Package Create on Debug
- 2021-12-17
- Change Content to Release
- Fix Changelog
- Add Example To Release

## [0.0.2] / 2021-12-11
- Empty Text error (use `ShowText = false`)
- Add `<Revision>`
- Add Text to empty
- Fix Text on `NewPushButtonData`
- Add `AssemblyName` on Develop Version
- Fix Unique Name Problem
- Add `ricaun.Revit.UI.Example` Project
- Try not valid Button Item
- Add Scale ImageSource
- Add RibbonItemExtension
- Fix net47 to net46

## [0.0.1] / 2021-12-11
- Fix Release / Content Build folder
- Add README / LICENSE
- Add Build Project
- Add LanguageExtension
- Add RibbonPanelExtension
- Add Scale on BitmapExtension
- Add Base64 on BitmapExtension
- Add BitmapExtension
- Add AutodeskExtension
- First Release

[vNext]: ../../compare/1.0.0...HEAD
[0.6.3]: ../../compare/0.6.2...0.6.3
[0.6.2]: ../../compare/0.6.1...0.6.2
[0.6.1]: ../../compare/0.6.0...0.6.1
[0.6.0]: ../../compare/0.5.7...0.6.0
[0.5.7]: ../../compare/0.5.6...0.5.7
[0.5.6]: ../../compare/0.5.5...0.5.6
[0.5.5]: ../../compare/0.5.4...0.5.5
[0.5.4]: ../../compare/0.5.3...0.5.4
[0.5.3]: ../../compare/0.5.2...0.5.3
[0.5.2]: ../../compare/0.5.1...0.5.2
[0.5.1]: ../../compare/0.5.0...0.5.1
[0.5.0]: ../../compare/0.4.0...0.5.0
[0.4.0]: ../../compare/0.3.2...0.4.0
[0.3.2]: ../../compare/0.3.1...0.3.2
[0.3.1]: ../../compare/0.3.0...0.3.1
[0.3.0]: ../../compare/0.2.1...0.3.0
[0.2.1]: ../../compare/0.2.0...0.2.1
[0.2.0]: ../../compare/0.1.2...0.2.0
[0.1.2]: ../../compare/0.1.1...0.1.2
[0.1.1]: ../../compare/0.1.0...0.1.1
[0.1.0]: ../../compare/0.0.9...0.1.0
[0.0.9]: ../../compare/0.0.8...0.0.9
[0.0.8]: ../../compare/0.0.7...0.0.8
[0.0.7]: ../../compare/0.0.6...0.0.7
[0.0.6]: ../../compare/0.0.5...0.0.6
[0.0.5]: ../../compare/0.0.4...0.0.5
[0.0.4]: ../../compare/0.0.3...0.0.4
[0.0.3]: ../../compare/0.0.2...0.0.3
[0.0.2]: ../../compare/0.0.1...0.0.2
[0.0.1]: ../../compare/0.0.1