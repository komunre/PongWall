using Robust.Shared.Map;
using System.Collections.Generic;
using Robust.Shared.IoC;
using Robust.Client.Player;
using System;
using Robust.Shared.GameObjects;
using Robust.Shared.Maths;
using Robust.Client.GameObjects;

namespace Robust.PongWall
{
    public class PongWallSystem : EntitySystem
    {
        [Dependency] private readonly IMapManager _mapManager;
        //[Dependency] private readonly IPlayerManager _playerManager;
        [Dependency] private readonly IPlayerManager _playerManager;
        [Dependency] private readonly IEntityManager _entityManager;
        private List<IEntity> _wallBlocks = new List<IEntity>();
        private IEntity _ball;
        private IEntity _paddle;
        private MapId _map;
        private Vector2 ArenaSize = new Vector2(200, 200);

        public PongWallSystem()
        {
        }

        public void Initialize() {
            base.Initialize();

            //SubscribeLocalEven<PlayerAttachSysMessage>()
        }

        public void StartGame() {
            _wallBlocks.Clear();

            _map = _mapManager.CreateMap();

            for (int y = 0; y < 5; y++) {
                for (int x = 0; x < 10; x++) {
                    _wallBlocks.Add(_entityManager.SpawnEntity("Wall", new MapCoordinates(y * 10, x * 20, _map)));
                }
            }

            _paddle = _entityManager.SpawnEntity("Paddle", new MapCoordinates(new Vector2(200, 180), _map));

            var player = _playerManager.LocalPlayer;
            player.AttachEntity(_paddle);

            var camera = _entityManager.SpawnEntity(null, new MapCoordinates(ArenaSize/2f, _map));
            var eye = camera.AddComponent<EyeComponent>();
            eye.Current = true;
            eye.Zoom = Vector2.One;
        }
    }
}