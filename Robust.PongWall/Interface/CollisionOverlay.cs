using Robust.Client.Graphics;
using Robust.Shared.IoC;
using Robust.Client.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.GameObjects;
using Robust.Shared.Maths;

namespace Robust.PongWall.Interface
{
    public class CollisionOverlay : Overlay
    {
        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
        [Dependency] private readonly IComponentManager _componentManager = default!;
        private ShaderInstance _shader;

        public CollisionOverlay() {
            _shader = _prototypeManager.Index<ShaderPrototype>("unshaded").Instance();
        }
        protected override void Draw(in OverlayDrawArgs args) {
            var handle = args.WorldHandle;

            handle.UseShader(_shader);

            foreach (var physics in _componentManager.EntityQuery<PhysicsComponent>()) {
                var aabb = physics.GetWorldAABB();

                handle.DrawRect(aabb, Color.White, true);
            }

            handle.UseShader(null);
        }
    }
}