using Nuke.Common;

namespace Components;

public interface IPack : INukeBuild
{
    Target Pack { get; }
}
