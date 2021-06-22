using Robust.Shared.Map;
using System.Collections.Generic;
using Robust.Shared.IoC;
using Robust.Client.Player;
using System;
using Robust.Shared.GameObjects;
using Robust.Shared.Maths;
using Robust.Client.GameObjects;
using Robust.Shared.Log;

namespace Content.PongWall
{
    public class PongWallSystem : EntitySystem
    {
        [Dependency] private readonly IMapManager _mapManager = default!;
        //[Dependency] private readonly IPlayerManager _playerManager;
        [Dependency] private readonly IPlayerManager _playerManager = default!;
        [Dependency] private readonly IEntityManager _entityManager = default!;
        private List<IEntity> _wallBlocks = new List<IEntity>();
        private IEntity _ball;
        private IEntity _paddle;
        private MapId _map;
        private Vector2 ArenaSize;
        private int _blocksX = 10;
        private int _blocksY = 5;

        public PongWallSystem()
        {
        }

        public override void Initialize() {
            base.Initialize();

            //SubscribeLocalEven<PlayerAttachSysMessage>()
            ArenaSize = new Vector2(_blocksX * 2.2f, 100f);
        }

        public void StartGame() {
            Logger.Info("Starting game...");

            _wallBlocks.Clear();

            _map = _mapManager.CreateMap();

            for (int y = 0; y < _blocksY; y++) {
                for (int x = 0; x < _blocksX; x++) {
                    _wallBlocks.Add(_entityManager.SpawnEntity("Wall", new MapCoordinates(x * 2.2f, y * 1.2f + 50f, _map)));
                }
            }

            _paddle = _entityManager.SpawnEntity("Paddle", new MapCoordinates(new Vector2(_blocksX * 2.2f / 2f, ArenaSize.Y / 2f - 7.5f), _map));

            var player = _playerManager.LocalPlayer;
            player.AttachEntity(_paddle);

            var camera = _entityManager.SpawnEntity(null, new MapCoordinates(new Vector2(_blocksX * 2.2f / 2f, ArenaSize.Y / 2f), _map));
            var eye = camera.AddComponent<EyeComponent>();
            eye.Current = true;
            eye.Zoom = Vector2.One;

            Logger.Info("Game started");
        }
    }
}