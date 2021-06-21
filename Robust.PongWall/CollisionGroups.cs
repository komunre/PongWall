using Robust.Shared.Physics.Dynamics;
using Robust.Shared.Serialization;
using System;

namespace Robust.PongWall
{
    [Flags]
    [FlagsFor(typeof(CollisionLayer)), FlagsFor(typeof(CollisionMask))]
    public enum CollisionGroups
    {
        None = 0,
        Solid = 1 << 0,
    }
}