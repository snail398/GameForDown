
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assets.Scripts.Pool;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Obstacles
{
    public class ObstacleGenerator
    {
        public struct Ctx
        {
            public int lineCount;
            public int spawnTime;
            public List<string> obstaclePrefabs;
            public GameObject parent;
            public ObstaclePositionFinder positionFinder;
        }

        private const int StartPosY = 150;
        private readonly Ctx _ctx;

        public ObstacleGenerator(Ctx ctx)
        {
            _ctx = ctx;
            Observable.Timer(System.TimeSpan.FromSeconds(_ctx.spawnTime))
                .Repeat()
                .Subscribe(_ => GenerateObstacle()).AddTo(_ctx.parent);
        }
        
        public void GenerateObstacle()
        {
            List<int> posList = _ctx.positionFinder.GetPositionsForObstacles();
            foreach (var pos in posList)
            {
                PoolManager.GetObject("Wall", new Vector3(pos, StartPosY, 0), Quaternion.identity);
            }

            if (posList.Count.Equals(_ctx.lineCount - 1))
            {
                List<int> uniqWallPos = _ctx.positionFinder.PossiblePosList.Except(posList).ToList();
                PoolManager.GetObject(_ctx.obstaclePrefabs[Random.Range(0, _ctx.obstaclePrefabs.Count)], new Vector3(uniqWallPos[0], StartPosY, 0), Quaternion.identity);
            }
        }
    }
}
