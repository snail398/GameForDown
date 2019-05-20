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
            public GameEvent onTutorialStart;
            public GameEvent onTutorialFinish;
        }

        private const int StartPosY = 150;
        private const int Steps = 5;

        private readonly Ctx _ctx;
        private StepSuccesful currentInst;
        private int _tutorialStep;
        private List<int> posList;

        public TutorialController(Ctx ctx)
        {
            _ctx = ctx;
            StartTutorial();
        }

        private void StartTutorial()
        {
            _ctx.onTutorialStart?.Raise();
            posList = _ctx.positionFinder.GetPossiblePositionForObstacle();

            _ctx.view.ShowText("Lets start tutorial \n Swipe right");

            _tutorialStep++;
            CreateTutorialEnviroment();
        }

        private void TutorialFinish()
        {
            _ctx.view.ShowText("Good Luck!");
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
                CreateTutorialEnviroment();
            }
        }
        private void CreateTutorialEnviroment()
        {
            switch (_tutorialStep)
            {
                case 1:
                    _ctx.view.ShowArrow("left");
                    PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[0], StartPosY, 0), Quaternion.identity);
                    PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 1], StartPosY, 0), Quaternion.identity);
                    PoolManager.GetObject("Wall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 2], StartPosY + 30, 0), Quaternion.identity);
                    PoolManager.GetObject("Wall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 1], StartPosY + 30, 0), Quaternion.identity);
                    currentInst = Instantiate(_ctx.stepSuccesful, Vector3.zero, Quaternion.identity);
                    currentInst.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[2], StartPosY + 50, 0));
                    currentInst.stepComplite.Subscribe(_ => NextStep()).AddTo(_ctx.view);
                    break;
                case 2:
                    _ctx.view.ShowArrow("right");
                    _ctx.view.ShowText("Swipe right");
                    PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[0], StartPosY, 0), Quaternion.identity);
                    PoolManager.GetObject("LongWall", new Vector3(_ctx.positionFinder.PossiblePosList[_ctx.positionFinder.PossiblePosList.Count - 1], StartPosY, 0), Quaternion.identity);
                    PoolManager.GetObject("Wall", new Vector3(_ctx.positionFinder.PossiblePosList[1], StartPosY + 30, 0), Quaternion.identity);
                    PoolManager.GetObject("Wall", new Vector3(_ctx.positionFinder.PossiblePosList[0], StartPosY + 30, 0), Quaternion.identity);
                    currentInst.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[2], StartPosY + 50, 0));
                    break;
                case 3:
                    _ctx.view.ShowArrow("down");
                    _ctx.view.ShowText("Swipe down for change color");
                    currentInst.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[2], StartPosY + 10, 0));
                    for (int i = 0; i < posList.Count; i++)
                    {
                        if (i == 0)
                        {
                            PoolManager.GetObject("ColorWall", new Vector3(posList[i], StartPosY, 0), Quaternion.identity);
                            continue;
                        }
                        PoolManager.GetObject("Wall", new Vector3(posList[i], StartPosY, 0), Quaternion.identity);
                    }
                    break;
                case 4:
                    _ctx.view.ShowText("Swipe up for change shape");
                    _ctx.view.ShowArrow("up");
                    currentInst.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[2], StartPosY + 10, 0));
                    for (int i = 0; i < posList.Count; i++)
                    {
                        if (i == 2)
                        {
                            PoolManager.GetObject("ShapeWall", new Vector3(posList[i], StartPosY, 0), Quaternion.identity);
                            continue;
                        }
                        PoolManager.GetObject("Wall", new Vector3(posList[i], StartPosY, 0), Quaternion.identity);
                    }
                    break;
                case 5:
                    _ctx.view.ShowText("Choose right shape and color");
                    _ctx.view.HideArrow();
                    currentInst.SetPosition(new Vector3(_ctx.positionFinder.PossiblePosList[2], StartPosY + 10, 0));
                    for (int i = 0; i < posList.Count; i++)
                    {
                        if (i == 1)
                        {
                            PoolManager.GetObject("ColorAndShapeWall", new Vector3(posList[i], StartPosY, 0), Quaternion.identity);
                            continue;
                        }
                        PoolManager.GetObject("Wall", new Vector3(posList[i], StartPosY, 0), Quaternion.identity);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
