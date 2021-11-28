using Components;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Notifications;

[AzurePipelines(
    AzurePipelinesImage.UbuntuLatest,
    InvokedTargets = new[] { nameof(IPack.Pack) },
    NonEntryTargets = new[] { nameof(Restore), nameof(Compile) },
    ImportSecrets = new[] { Notifications.HostVariableName, Notifications.TokenVariableName })]
partial class Build
{
}
