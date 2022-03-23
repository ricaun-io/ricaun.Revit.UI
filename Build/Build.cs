using Nuke.Common;
using Nuke.Common.Execution;
using ricaun.Nuke;
using ricaun.Nuke.Components;

[CheckBuildProjectConfigurations]
class Build : NukeBuild, IPublishPack, IRevitPackageBuilder
{
    //string IHazPackageBuilderProject.Name => this.From<IHazExample>().Name;
    string IHazPackageBuilderProject.Name => "Example";
    string IHazRevitPackageBuilder.Application => "Revit.App";
    //bool IHazExample.ReleaseExample => false;
    public static int Main() => Execute<Build>(x => x.From<IPublishPack>().Build);
}