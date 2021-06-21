using Robust.Client;

namespace Content.PongWall
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            ContentStart.StartLibrary(args, new GameControllerOptions()
            {
                // DEVNOTE: Your options here!
                
                // We disable sandboxing given we're using RobustToolbox as a library, and we won't be on the hub.
                Sandboxing = false,
                
                // Projects with this prefix will be loaded by the engine.
                ContentModulePrefix = "Content.",
                
                // Name of the folder where the game will be built in. Also check Content.PongWall.csproj:9!
                ContentBuildDirectory = "Content.PongWall",
                
                // Default window name. This can also be changed on runtime with the IClyde service.
                DefaultWindowTitle = "Pong Wall",
                
                // This template is singleplayer-only, so we disable connecting to a server from program arguments.
                DisableCommandLineConnect = true,
                
                // Name of the folder where the user's data (config, etc) will be stored.
                UserDataDirectoryName = "PongWall",
                
                // Name of the configuration file in the user's data directory.
                ConfigFileName = "config.toml",
                
                // There are a few more options, be sure to check them all!
            });
        }
    }
}