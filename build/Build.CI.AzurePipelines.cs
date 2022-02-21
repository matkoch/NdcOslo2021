using Components;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Notifications;

[AzurePipelines(
    AzurePipelinesImage.UbuntuLatest,
    InvokedTargets = new[] { nameof(Compile) },
    NonEntryTargets = new[] { nameof(Restore) },
    ImportSecrets = new[] { Notifications.HostVariableName, Notifications.TokenVariableName })]
partial class Build
{
}
