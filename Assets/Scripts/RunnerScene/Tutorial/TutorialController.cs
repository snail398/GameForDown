using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Obstacles;
using Assets.Scripts.Pool;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        public struct Ctx
        {
            public Action onTutorialFinished;
            public TutorialView view;
            public ObstaclePositionFinder positionFinder;
            public StepSuccesful stepSuccesful;
            public SlowDowner SlowDowner;
            public GameEvent onTutorialStart;
            public GameEvent onTutorialFinish;
            public ReactiveProperty<int> blockSwipeCommand;
            public ReactiveCommand hideTutorialView;
            public PlayersData playersData;
        }

        private const int StartPosY = 50;
        private const int Steps = 5;

        private readonly Ctx _ctx;
        private StepSuccesful currentInst;
        private SlowDowner currentSlowDowner;
        private int _tutorialStep;
        private List<int> posList;

        public TutorialController(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.hideTutorialView.Subscribe(_=>
            {
                _ctx.view.HideArrow();
                _ctx.view.HideText();
            }).AddTo(_ctx.view);
            StartTutorial();
        }

        private void StartTutorial()
        {
            posList = _ctx.positionFinder.GetPossiblePositionForObstacle();
            CreateTutorialLevel();
            _ctx.onTutorialStart?.Raise();

            _ctx.view.ShowText("Lets start tutorial");
            _ctx.view.HideArrow();
            //_tutorialStep++;
            SetHint();
        }

        private void TutorialFinish()
        {
            _ctx.view.ShowText("Good Luck!");
            _ctx.playersData.SetRunTutorialPassed();
            _ctx.onTutorialFinish?.Raise();
            _ctx.onTutorialFinished?.Invoke();
        }

        private void NextStep()
        {
            _tutorialStep++;
            if (_tutorialStep > Steps)
            {
                TutorialFinish();
            }
            else
            {
                SetHint();
            }
        }

        private void CreateTutorialLevel()
        {
           // currentInst = Instantiate(_ctx.stepSuccesful, Vector3.zero, Quaternion.identity);

            currentSlowDowner = Instantiate(_ctx.SlowDowner, Vector3.zero, Quaternion.identity);
            currentSlowDowner.GetComponent<StepSuccesful>().stepComplite.Subscribe(_ => NextStep()).AddTo(_ctx.view);

            PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[0], 20, 0), Quaternion.identity);
            PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 1], 20, 0), Quaternion.identity);

            //step 1
            PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[0], StartPosY, 0), Quaternion.identity);
            PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 1], StartPosY, 0), Quaternion.identity);
            PoolManager.GetObject("Wall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 2], StartPosY + 30, 0), Quaternion.identity);
            PoolManager.GetObject("Wall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 1], StartPosY + 30, 0), Quaternion.identity);
            //step 2
            PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 2], StartPosY+70, 0), Quaternion.identity);
            PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 1], StartPosY+70, 0), Quaternion.identity);
            PoolManager.GetObject("Wall", new Vector3(_ctx.positionFinder.PossiblePosList[2], StartPosY + 100, 0), Quaternion.identity);
            PoolManager.GetObject("Wall", new Vector3(_ctx.positionFinder.PossiblePosList[0], StartPosY + 100, 0), Quaternion.identity);
            //step 3
            for (int i = 0; i < posList.Count; i++)
            {
                if (i == 1)
                {
                    ColorObstacleController colorWall = PoolManager.GetObject("ColorWall", new Vector3(posList[i], StartPosY+140, 0), Quaternion.identity).GetComponent<ColorObstacleController>();
                    colorWall.SetColor(1);
                    continue;
                }
                PoolManager.GetObject("Wall", new Vector3(posList[i], StartPosY+140, 0), Quaternion.identity);
            }
            //step 4
            for (int i = 0; i < posList.Count; i++)
            {
                if (i == 1)
                {
                    ShapeObstacleController shapeWall = PoolManager.GetObject("ShapeWall", new Vector3(posList[i], StartPosY+210, 0), Quaternion.identity).GetComponent<ShapeObstacleController>();
                    shapeWall.SetMesh(1);
                    continue;
                }
                PoolManager.GetObject("Wall", new Vector3(posList[i], StartPosY+210, 0), Quaternion.identity);
            }
            //step 5
            for (int i = 0; i < posList.Count; i++)
            {
                if (i == 1)
                {
                    ColorAndShapeObstacleController complexWall = PoolManager.GetObject("ColorAndShapeWall", new Vector3(posList[i], StartPosY+280, 0), Quaternion.identity).GetComponent<ColorAndShapeObstacleController>();
                    complexWall.SetColor(0);
                    complexWall.SetMesh(0);
                    continue;
                }
                PoolManager.GetObject("Wall", new Vector3(posList[i], StartPosY+280, 0), Quaternion.identity);
            }
        }

        private void SetHint()
        {
            switch (_tutorialStep)
            {
                case 0:
                    currentSlowDowner.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[1], StartPosY , 0));
                    break;
                case 1:
                    _ctx.blockSwipeCommand.SetValueAndForceNotify(4);
                    _ctx.view.ShowArrow("left");
                    _ctx.view.ShowText("Swipe left");
                    currentSlowDowner.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[1], StartPosY+18, 0));
                    break;
                case 2:
                    _ctx.blockSwipeCommand.SetValueAndForceNotify(2);
                    _ctx.view.ShowArrow("right");
                    _ctx.view.ShowText("Swipe right");
                    currentSlowDowner.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[1], 30, 0));
                    break;
                case 3:
                    _ctx.blockSwipeCommand.SetValueAndForceNotify(3);
                    _ctx.view.ShowArrow("down");
                    _ctx.view.ShowText("Swipe down for change color");
                    currentSlowDowner.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[1], 45, 0));
                    break;
                case 4:
                    _ctx.blockSwipeCommand.SetValueAndForceNotify(1);
                    _ctx.view.ShowText("Swipe up for change shape");
                    _ctx.view.ShowArrow("up");
                    currentSlowDowner.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[1], 70, 0));
                    break;
                case 5:
                    _ctx.blockSwipeCommand.SetValueAndForceNotify(0);
                    _ctx.view.ShowText("Choose right shape and color");
                    _ctx.view.HideArrow();
                    currentSlowDowner.SetNoSlowDown();
                    currentSlowDowner.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[1], 70, 0));
                    break;
                default:
                    break;
            }
        }
    }
}
