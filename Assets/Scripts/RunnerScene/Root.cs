using System;
using Assets.Scripts.Coins;
using Assets.Scripts.HeroFolder;
using Assets.Scripts.Obstacles;
using Assets.Scripts.Pool;
using Assets.Scripts.Score;
using Assets.Scripts.Tutorial;
using UniRx;
using UnityEngine;

namespace Assets.Scripts
{
    public class Root
    {
        public struct Ctx
        {
            public Action restartScene;
            public Action returnToStoryScene;
        }
        private RootView _rootView;
        private Ctx _ctx;

        private Hero _mainHero;
        private ObstacleGenerator _obstacleGenerator;
        private CoinGenerator _coinGenerator;
        private ObstaclePositionFinder _positionFinder;
        private ScoreContainer _scoreContainer;
        private TutorialController _tutorial;
        private ReactiveProperty<int> _blockSwipeCommand;
        private ReactiveCommand _hideTutorialView;

        public Root(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void InitializeRunnerRoot()
        {
            _rootView = GameObject.Find("Root").GetComponent<RootView>();
            RootView.Ctx rootViewCtx = new RootView.Ctx
            {
                returnToStory = _ctx.returnToStoryScene,
            };
            _rootView.SetCtx(rootViewCtx);
            _blockSwipeCommand = new ReactiveProperty<int>().AddTo(_rootView);
            _hideTutorialView = new ReactiveCommand().AddTo(_rootView);
            CreateHero();
            CreatePositionFinder();
            CreateGameSpeedController();
            CreateScoreContainer();
            // _rootView.GetComponent<PoolSetup>().onPoolReady.Subscribe(_ => OnPoolReady()).AddTo(_rootView);
            OnPoolReady();
        }

        public void OnPoolReady()
        {
            if (CheckTutorial())
                CreateTutorial();
            else
            {
                CreateObstacleGenerator();
                CreateCoinGenerator();
            }
        }

        private void CreatePositionFinder()
        {
            ObstaclePositionFinder.Ctx positionFinderCtx = new ObstaclePositionFinder.Ctx
            {
                lineCount = _rootView.LevelConfig.LineCount,
                lineWidth = _rootView.LevelConfig.LineWidth,
            };
            _positionFinder = new ObstaclePositionFinder(positionFinderCtx);
        }

        private void CreateTutorial()
        {
            TutorialController.Ctx tutorialCtx = new TutorialController.Ctx
            {
                onTutorialFinished = () =>
                {
                    CreateObstacleGenerator();
                    CreateCoinGenerator();
                },
                view = UnityEngine.Object.Instantiate(_rootView.TutorialPrefab, Vector3.zero,Quaternion.identity),
                positionFinder =  _positionFinder,
                stepSuccesful = _rootView.StepSuccesful,
                onTutorialFinish = _rootView.OnTutorialFinish,
                onTutorialStart = _rootView.OnTutorialStart,
                SlowDowner = _rootView.SlowDowner,
                blockSwipeCommand = _blockSwipeCommand,
                hideTutorialView = _hideTutorialView,
            };
           _tutorial = new TutorialController(tutorialCtx);
        }
        
        private void CreateScoreContainer()
        {
            ScoreContainer.Ctx scoreCtx = new ScoreContainer.Ctx
            {
                score = _rootView.Score,
                maxScore = _rootView.MaxScore,
                parent = _rootView.gameObject,
            };
            _scoreContainer = new ScoreContainer(scoreCtx);
        }

        private void CreateGameSpeedController()
        {
            GameSpeed.Ctx gameSpeedCtx = new GameSpeed.Ctx
            {
                speedIncreaseTime = _rootView.LevelConfig.SpeedIncreaseTime,
                startGameSpeed = _rootView.LevelConfig.StartGameSpeed,
                speedIncreaseCount = _rootView.LevelConfig.SpeedIncreaseCount,
            };
            _rootView.GameSpeed.Initialize(gameSpeedCtx);
        }

        private void CreateObstacleGenerator()
        {
            ObstacleGenerator.Ctx obsGenCtx = new ObstacleGenerator.Ctx
            {
                lineCount = _rootView.LevelConfig.LineCount,
                spawnTime = _rootView.LevelConfig.SpawnTime,
                obstaclePrefabs = _rootView.LevelConfig.WallPrefabNames,
                parent = _rootView.gameObject,
                positionFinder = _positionFinder,
            };
            _obstacleGenerator = new ObstacleGenerator(obsGenCtx);
        }

        private void CreateCoinGenerator()
        {
            CoinGenerator.Ctx coinCtx = new CoinGenerator.Ctx
            {
                lineCount = _rootView.LevelConfig.LineCount,
                spawnTime = _rootView.LevelConfig.SpawnTime,
                parent = _rootView.gameObject,
                positionFinder = _positionFinder,
            };
            _coinGenerator = new CoinGenerator(coinCtx);
        }

        private void CreateHero()
        {
            HeroView view = UnityEngine.Object.Instantiate(_rootView.HeroPrefab, Vector2.zero, Quaternion.identity);
            if (_mainHero == null)
            {
                Hero.Ctx heroCtx = new Hero.Ctx
                {
                    lineCount = _rootView.LevelConfig.LineCount,
                    maxHeath = 3,
                    currentHealth = 3,
                    view = view,
                    availableColors = _rootView.LevelConfig.AvailableColors,
                    availableMeshes = _rootView.LevelConfig.AvailableMeshes,
                    lineWidth = _rootView.LevelConfig.LineWidth,
                    continueGame = _rootView.GameSpeed.ContinueGame,
                    blockSwipeCommand = _blockSwipeCommand,
                    hideTutorialView = _hideTutorialView,
                    restartScene = _ctx.restartScene,
                    returnToStoryScene = _ctx.returnToStoryScene,
                };
                _mainHero = new Hero(heroCtx);
            }
            _mainHero.InitializeHero();
            ConfigCamera(view.transform);
        }

        public void OnHeroHeath()
        {
            _scoreContainer.OnHeroDeath();
        }

        private void ConfigCamera(Transform hero)
        {
            _rootView.CameraController.SetHero(hero);
        }

        private bool CheckTutorial()
        {
            //   return true;
            return PlayerPrefs.GetInt("tutorial_passed").Equals(0);
        }

        public void GameRefresh()
        {
            _rootView.Score.Value = 0;
        }
    }
}
