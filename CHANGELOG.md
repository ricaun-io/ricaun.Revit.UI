# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

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