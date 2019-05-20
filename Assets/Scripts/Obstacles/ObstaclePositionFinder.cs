using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class ObstaclePositionFinder 
    {
        public struct Ctx
        {
            public int lineCount;
            public int lineWidth;
        }

        private readonly Ctx _ctx;
        private readonly List<int> _possiblePosList;

        public List<int> PossiblePosList => _possiblePosList;

        public ObstaclePositionFinder(Ctx ctx)
        {
            _ctx = ctx;
            _possiblePosList = GetPossiblePositionForObstacle();
        }

        public List<int> GetPositionsForObstacles()
        {
            List<int> posList = new List<int>();
            for (int i = 0; i < _ctx.lineCount - 1; i++)
            {
                var alreadyHas = false;
                int tryPos = _possiblePosList[Random.Range(0, _possiblePosList.Count)];
                foreach (var pos in posList)
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
            int temp = -_ctx.lineWidth * _ctx.lineCount / 2;
            while (temp < _ctx.lineCount)
            {
                posList.Add(temp + 1);
                temp += _ctx.lineWidth;
            }
            return posList;
        }

    }
}
