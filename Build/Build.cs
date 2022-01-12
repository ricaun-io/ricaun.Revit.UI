using Nuke.Common;
using Nuke.Common.Execution;
using ricaun.Nuke;
using ricaun.Nuke.Components;

[CheckBuildProjectConfigurations]
class Build : NukeBuild, IPublishPack, ICompileExample, IRevitPackageBuilder
{
    string IHazPackageBuilderProject.Name => this.From<IHazExample>().Name;
    string IHazRevitPackageBuilder.Application => "Revit.App";
    bool IHazRevitPackageBuilder.NewVersions => true;
    bool IHazExample.ReleaseExample => false;
    public static int Main() => Execute<Build>(x => x.From<IPublishPack>().Build);
}