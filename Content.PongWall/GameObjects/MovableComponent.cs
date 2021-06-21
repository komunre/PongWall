using Robust.Shared.GameObjects;
using System.Collections.Generic;
using Robust.Shared.Input;
using Robust.Shared.Physics.Controllers;
using Robust.Shared.IoC;
using Robust.Shared.Maths;
using Robust.Shared.Log;

namespace Content.PongWall.GameObjects
{
    public class MovableComponent : Component
    {
        public override string Name => "Movable";

    
    }
    
    public class MovableController : VirtualController {
        public override void UpdateBeforeSolve(bool prediction, float frameTime) {
            base.UpdateBeforeSolve(prediction, frameTime);

            foreach (var (paddle, physics) in ComponentManager.EntityQuery<PaddleComponent, PhysicsComponent>()) {
                var speed = paddle.Speed;

                var direction = Vector2.Zero;
                if (paddle.Pressed == PaddleSystem.Button.Right)
                    direction += Vector2.UnitX;

                if (paddle.Pressed == PaddleSystem.Button.Left)
                    direction -= Vector2.UnitX;

                physics.LinearVelocity = direction * speed;
            }
        }
    }
}