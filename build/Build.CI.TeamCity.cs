using Components;
using Nuke.Common.CI.TeamCity;
using Nuke.Notifications;

[TeamCity(
    VcsTriggeredTargets = new[] { nameof(Compile) },
    NonEntryTargets = new[] { nameof(Restore) },
    ImportSecrets = new[] { Notifications.HostVariableName, Notifications.TokenVariableName })]
[TeamCityToken(Notifications.HostVariableName, "24f9009b-0fe2-443f-a1b7-7597d1746a7f")]
[TeamCityToken(Notifications.TokenVariableName, "59f32c60-f071-43a8-b315-403063f89304")]
partial class Build
{
}
