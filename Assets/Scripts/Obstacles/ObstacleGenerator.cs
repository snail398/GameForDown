
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
        }
        private const int StartPosY = 15;
        private readonly List<int> _possiblePosList;
        private readonly Ctx _ctx;
        private readonly IDisposable _timerHandler;

        public ObstacleGenerator(Ctx ctx)
        {
            _ctx = ctx;
            _possiblePosList = GetPossiblePositionForObstacle();
            GenerateObstacle();
            _timerHandler?.Dispose();
            _timerHandler = Observable.Timer(System.TimeSpan.FromSeconds(_ctx.spawnTime)).Repeat().Subscribe(_ => GenerateObstacle());
        }

        public void GenerateObstacle()
        {
            List<int> posList = GetPositionsForObstacles();
            foreach (var pos in posList)
            {
                PoolManager.GetObject("Wall", new Vector3(pos, StartPosY, 0), Quaternion.identity);
            }

            if (posList.Count.Equals(_ctx.lineCount - 1))
            {
                List<int> uniqWallPos = _possiblePosList.Except(posList).ToList();
                PoolManager.GetObject(_ctx.obstaclePrefabs[Random.Range(0, _ctx.obstaclePrefabs.Count)], new Vector3(uniqWallPos[0], StartPosY, 0), Quaternion.identity);
            }
        }

        public List<int> GetPositionsForObstacles()
        {
            List<int> posList = new List<int>();
            for (int i = 0; i < _ctx.lineCount - 1; i++)
            {
                var alreadyHas = false;
                int tryPos = _possiblePosList[Random.Range(0, _possiblePosList.Count)];
;               foreach (var pos in posList)
                {
                    if (!pos.Equals(tryPos)) continue;
                    alreadyHas = true;
                    break;
                }
                if (!alreadyHas) posList.Add(tryPos);
            }

            return posList;
        }

        public List<int> GetPossiblePositionForObstacle()
        {
            List<int> posList = new List<int>();
            int temp = -_ctx.lineCount;
            while (temp < _ctx.lineCount)
            {
                posList.Add(temp+1);
                temp += 2;
            }
            return posList;
        }
    }
}
