using Assets.Scripts.HeroFolder;
using Assets.Scripts.Obstacles;
using UnityEngine;

namespace Assets.Scripts
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private GameSpeed _gameSpeed;
        [SerializeField] private HeroView _heroPrefab;

        private Hero _mainHero;
        private float _tempTime;
        private ObstacleGenerator _obstacleGenerator;
        
        void Awake()
        {
            CreateHero();
            CreateObstacleGenerator();
            CreateGameSpeedController();
        }

        private void CreateGameSpeedController()
        {
            GameSpeed.Ctx gameSpeedCtx = new GameSpeed.Ctx
            {
                speedIncreaseTime = _levelConfig.SpeedIncreaseTime,
                startGameSpeed = _levelConfig.StartGameSpeed,
                speedIncreaseCount = _levelConfig.SpeedIncreaseCount,
            };
            _gameSpeed.Initialize(gameSpeedCtx);
        }

        private void CreateObstacleGenerator()
        {
            ObstacleGenerator.Ctx obsGenCtx = new ObstacleGenerator.Ctx
            {
                lineCount = _levelConfig.LineCount,
                spawnTime = _levelConfig.SpawnTime,
                obstaclePrefabs = _levelConfig.WallPrefabNames,
            };
            _obstacleGenerator = new ObstacleGenerator(obsGenCtx);
        }

        private void CreateHero()
        {
            Hero.Ctx heroCtx = new Hero.Ctx
            {
                lineCount = _levelConfig.LineCount,
                maxHeath = 3,
                currentHealth = 3,
                view = Instantiate(_heroPrefab, Vector2.zero, Quaternion.identity),
                availableColors = _levelConfig.AvailableColors,
                availableMeshes = _levelConfig.AvailableMeshes,
            };
            _mainHero = new Hero(heroCtx);
        }
    }
}
