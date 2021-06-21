using Robust.Shared.GameObjects;
using System.Collections.Generic;
using Robust.Shared.Input;
using Robust.Shared.Physics.Controllers;
using Robust.Shared.IoC;
using Robust.Shared.Maths;
using Robust.Shared.Log;
using Robust.Client.Player;

namespace Content.PongWall.GameObjects
{
    public class MovableComponent : Component
    {
        public override string Name => "Movable";

    
    }
    
    public class MovableController : VirtualController {
        [Dependency] private readonly IPlayerManager _playerManager;
        public override void UpdateBeforeSolve(bool prediction, float frameTime) {
            base.UpdateBeforeSolve(prediction, frameTime);

            //foreach (var (paddle, physics) in ComponentManager.EntityQuery<PaddleComponent, PhysicsComponent>()) {
                if (!_playerManager.LocalPlayer.ControlledEntity.TryGetComponent<PaddleComponent>(out var paddle))
                    return;
                
                if (!_playerManager.LocalPlayer.ControlledEntity.TryGetComponent<PhysicsComponent>(out var physics))
                    return;

                var speed = paddle.Speed;

                var direction = Vector2.Zero;
                if (paddle.Pressed == PaddleSystem.Button.Right)
                    direction += Vector2.UnitX;

                if (paddle.Pressed == PaddleSystem.Button.Left)
                    direction -= Vector2.UnitX;

                physics.LinearVelocity = direction * speed;
            //}
        }
    }
}