using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Components;

public interface IPublish : INukeBuild
{
    IEnumerable<AbsolutePath> PackageFiles { get; }

    [Parameter] [Secret] string ApiKey => TryGetValue(() => ApiKey);

    Target Publish => _ => _
        .DependsOn<IPack>()
        .Requires(() => ApiKey)
        .Executes(() =>
        {
            DotNetNuGetPush(_ => _
                .SetSource("https://api.nuget.org/v3/index.json")
                .SetApiKey(ApiKey)
                .CombineWith(PackageFiles, (_, package) => _
                    .SetTargetPath(package)));
        });
}
