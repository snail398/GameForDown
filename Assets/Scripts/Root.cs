﻿using Assets.Scripts.Camera;
using Assets.Scripts.HeroFolder;
using Assets.Scripts.Obstacles;
using Assets.Scripts.Pool;
using Assets.Scripts.Score;
using Assets.Scripts.Tutorial;
using UniRx;
using UnityEngine;

namespace Assets.Scripts
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private GameSpeed _gameSpeed;
        [SerializeField] private HeroView _heroPrefab;
        [SerializeField] private TutorialView _tutorialPrefab;
        [SerializeField] private StepSuccesful stepSuccesful;
        [SerializeField] private IntVariable score;
        [SerializeField] private IntVariable maxScore;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private GameEvent onTutorialStart;
        [SerializeField] private GameEvent onTutorialFinish;

        private Hero _mainHero;
        private float _tempTime;
        private ObstacleGenerator _obstacleGenerator;
        private ObstaclePositionFinder _positionFinder;
        private ScoreContainer _scoreContainer;
        private TutorialController _tutorial;

        void Awake()
        {
            CreateHero();
            CreatePositionFinder();
            CreateGameSpeedController();
            CreateScoreContainer();
            GetComponent<PoolSetup>().onPoolReady.Subscribe(_ => OnPoolReady()).AddTo(this);
        }

        public void OnPoolReady()
        {
            if (CheckTutorial())
                CreateTutorial();
            else
                CreateObstacleGenerator();
        }
        private void CreatePositionFinder()
        {
            ObstaclePositionFinder.Ctx positionFinderCtx = new ObstaclePositionFinder.Ctx
            {
                lineCount = _levelConfig.LineCount,
                lineWidth = _levelConfig.LineWidth,
            };
            _positionFinder = new ObstaclePositionFinder(positionFinderCtx);
        }

        private void CreateTutorial()
        {
            TutorialController.Ctx tutorialCtx = new TutorialController.Ctx
            {
                onTutorialFinished = CreateObstacleGenerator,
                view =  Instantiate(_tutorialPrefab,Vector3.zero,Quaternion.identity),
                positionFinder =  _positionFinder,
                stepSuccesful = stepSuccesful,
                onTutorialFinish = onTutorialFinish,
                onTutorialStart = onTutorialStart,
            };
           _tutorial = new TutorialController(tutorialCtx);
        }

        void Update()
        {
            _scoreContainer.Tick(Time.deltaTime);
        }

        private void CreateScoreContainer()
        {
            ScoreContainer.Ctx scoreCtx = new ScoreContainer.Ctx
            {
                score = score,
                maxScore = maxScore,
            };
            _scoreContainer = new ScoreContainer(scoreCtx);
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
                parent = this.gameObject,
                positionFinder = _positionFinder,
            };
            _obstacleGenerator = new ObstacleGenerator(obsGenCtx);
        }

        private void CreateHero()
        {
            HeroView view = Instantiate(_heroPrefab, Vector2.zero, Quaternion.identity);
            Hero.Ctx heroCtx = new Hero.Ctx
            {
                lineCount = _levelConfig.LineCount,
                maxHeath = 3,
                currentHealth = 3,
                view =view,
                availableColors = _levelConfig.AvailableColors,
                availableMeshes = _levelConfig.AvailableMeshes,
                lineWidth = _levelConfig.LineWidth,
            };
            _mainHero = new Hero(heroCtx);
            ConfigCamera(view.transform);
        }

        public void OnHeroHeath()
        {
            _scoreContainer.OnHeroDeath();
        }

        private void ConfigCamera(Transform hero)
        {
            cameraController.SetHero(hero);
        }

        private bool CheckTutorial()
        {
            return true;
        }

        public void GameRefresh()
        {
            score.Value = 0;
        }
    }
}
