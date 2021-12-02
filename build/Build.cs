using System.Collections.Generic;
using System.Linq;
using Components;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Notifications;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[Notifications(EnableCancellation = true)]
[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push },
    EnableGitHubContext = true,
    InvokedTargets = new[] { nameof(IPack.Pack) },
    ImportSecrets = new[] { Notifications.HostVariableName, Notifications.TokenVariableName })]
partial class Build : NukeBuild, IPack, IPublish
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true)] readonly Solution Solution;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {

        });

    Target Restore => _ => _
        .Executes(() =>
        {

            DotNet($"restore {Solution}");
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .When(IsServerBuild, _ => _
                    .EnableContinuousIntegrationBuild()
                    .EnableDeterministic()));

            var publishConfigurations =
                from project in new[] { Solution.DotNet_Eval }
                from framework in project.GetTargetFrameworks()
                select (project, framework);

            DotNetPublish(_ => _
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .CombineWith(publishConfigurations, (_, c) => _
                    .SetProject(c.project)
                    .SetFramework(c.framework)));
        });

    [GitVersion] readonly GitVersion GitVersion;
    AbsolutePath OutputDirectory => RootDirectory / "output";

    Target IPack.Pack => _ => _
        .DependsOn(Compile)
        .Produces(OutputDirectory / "*.nupkg")
        .Executes(() =>
        {
            DotNetPack(_ => _
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(GitVersion.NuGetVersionV2)
                .SetOutputDirectory(OutputDirectory)
                .EnableNoBuild());
        });

    IEnumerable<AbsolutePath> IPublish.PackageFiles => OutputDirectory.GlobFiles("*.nupkg");
}


