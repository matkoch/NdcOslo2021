using Components;
using Nuke.Common.CI.AppVeyor;
using Nuke.Notifications;

[AppVeyor(
    AppVeyorImage.UbuntuLatest,
    InvokedTargets = new[] { nameof(Compile) })]
[AppVeyorSecret(
    Notifications.TokenVariableName,
    "ma9vIzhc2M4tFXv6eMc96bZs29aQ+kn/D5p9a3wUxB0RNeqBFpq+7vz3b+e0Ojvm")]
[AppVeyorSecret(
    Notifications.HostVariableName,
    "1V/SfJ7gFv9zPLT4nVdfXrJ+aSvNJS3YLRuBCOWwec+KlqDnHuGDMv6PwW1TJtau")]
partial class Build
{
}
