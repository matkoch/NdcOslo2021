using Components;
using Nuke.Common.CI.AppVeyor;
using Nuke.Notifications;

[AppVeyor(
    AppVeyorImage.UbuntuLatest,
    InvokedTargets = new[] { nameof(IPack.Pack) })]
[AppVeyorSecret(
    Notifications.TokenVariableName,
    "1yO8I+O+P37tItVjl+MywWFTslE4IPkzLBXvhwFflAg0mRdXP9jcyfyILqBTdwOk")]
[AppVeyorSecret(
    Notifications.HostVariableName,
    "fdssEbE2DBBIVFoPFISqaCxD0E2KKyfEaUqbQz2AQ5vYbGrnYwVjdsP9vNZiYH9ddk93c2g95vXd22o3RLWpGQ==")]
partial class Build
{
}
