using System.Globalization;
using Robust.Client;
using Robust.Shared.ContentPack;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Localization;
using Robust.Shared.Timing;
using Robust.Client.Graphics;
using Content.PongWall.Interface;
using Robust.Shared.Physics.Controllers;
using Content.PongWall.GameObjects;
using Content.PongWall.Input;

// DEVNOTE: You can change the namespace and project name to whatever you want!
// Just make sure to consistently use a prefix across your different projects.
namespace Content.PongWall
{
    public class EntryPoint : GameClient
    {
        // See line 35. Controls the default game culture and language.
        // Robust calls this culture, but you might find it more fitting to call it the game
        // language. Robust doesn't support changing this mid-game. Load your config file early
        // if you want that.
        private const string Culture = "en-US";

        public override void PreInit()
        {
            base.PreInit();
            
            // Default to en-US.
            // DEVNOTE: If you want your game to be multi-regional at runtime, you'll need to 
            // do something more complicated here.
            IoCManager.Resolve<ILocalizationManager>().LoadCulture(new CultureInfo(Culture));
        }

        public override void Init()
        {
            var factory = IoCManager.Resolve<IComponentFactory>();

            // DEVNOTE: Registers all of your components.
            IoCManager.Register<PongWallSystem, PongWallSystem>();
            //IoCManager.Register<SharedInputSystem, SharedInputSystem>();
            //IoCManager.Register<PaddleSystem, PaddleSystem>();
            //IoCManager.Register<MovableController, MovableController>();
            factory.DoAutoRegistrations();

            TemplateIoC.Register();

            IoCManager.BuildGraph();

            // DEVNOTE: This is generally where you'll be setting up the IoCManager further, like the tile manager.
            IoCManager.Resolve<PongWallSystem>().Initialize();
            //IoCManager.Resolve<PaddleMovementSystem>().Initialize();
            //IoCManager.Resolve<PaddleSystem>().Initialize();
        }

        public override void PostInit()
        {
            base.PostInit();

            // DEVNOTE: Further setup..
            
            // DEVNOTE: This starts the singleplayer mode,
            // which means you can start creating entities, spawning things...
            // If you want to have a main menu to start the game from instead, use the StateManager.
            IoCManager.Resolve<IBaseClient>().StartSinglePlayer();
            IoCManager.Resolve<PongWallSystem>().StartGame();

            var overlayManager = IoCManager.Resolve<IOverlayManager>();

            overlayManager.AddOverlay(new CollisionOverlay());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            // DEVNOTE: You'll want to do a proper shutdown here.
        }

        public override void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs)
        {
            base.Update(level, frameEventArgs);

            // DEVNOTE: Game update loop goes here. Usually you'll want some independent GameTicker.
        }
    }

    
}