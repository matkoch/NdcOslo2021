using Components;
using Nuke.Common.CI.SpaceAutomation;
using Nuke.Notifications;

[SpaceAutomation(
    "continuous",
    "mcr.microsoft.com/dotnet/sdk:6.0",
    InvokedTargets = new[] { nameof(IPack.Pack) },
    ImportSecrets = new[] { Notifications.HostVariableName, Notifications.TokenVariableName })]
partial class Build
{
}
