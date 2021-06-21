using Robust.Shared.GameObjects;
using Robust.Shared.Input;
using Robust.Shared.Input.Binding;
using Robust.Shared.Players;
using Robust.Shared.Log;
using System;
namespace Content.PongWall.GameObjects
{
    [RegisterComponent]
    public class PaddleComponent : Component {
        public override string Name => "Paddle"; 

        public float Speed = 10f;
        public PaddleSystem.Button Pressed = PaddleSystem.Button.None;
    }

    public class PaddleSystem : EntitySystem {
        public enum Button {
            Right,
            Left,
            None,
        }
        public override void Initialize() {
            base.Initialize();

            CommandBinds.Builder
                .Bind(EngineKeyFunctions.MoveRight, new ButtonInputCmdHandler(Button.Right, SetMovementInput))
                .Bind(EngineKeyFunctions.MoveLeft, new ButtonInputCmdHandler(Button.Left, SetMovementInput))
                .Register<PaddleSystem>();
        }

        public static void SetMovementInput(ICommonSession session, Button button, bool state) {
            if (session.AttachedEntity == null || session.AttachedEntity.Deleted || !session.AttachedEntity.TryGetComponent<PaddleComponent>(out var paddle))
                return;

            if (state) {
                paddle.Pressed = button;
                Logger.Debug(String.Format("{0} is now pressed", button));
            }
            else
                paddle.Pressed = Button.None;
            
        }

        private sealed class ButtonInputCmdHandler : InputCmdHandler
        {

            public delegate void MoveDirectionHandler(ICommonSession session, Button button, bool state);
            
            private readonly Button _button;
            private readonly MoveDirectionHandler _handler;
            
            public ButtonInputCmdHandler(Button button, MoveDirectionHandler handler)
            {
                _button = button;
                _handler = handler;
            }
            
            public override bool HandleCmdMessage(ICommonSession session, InputCmdMessage message)
            {
                if (message is not FullInputCmdMessage full)
                    return false;

                _handler.Invoke(session, _button, full.State == BoundKeyState.Down);
                return false;
            }
        }
    }

    
}