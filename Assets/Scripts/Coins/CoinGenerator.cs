using Assets.Scripts.Obstacles;
using Assets.Scripts.Pool;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Coins
{
    public class CoinGenerator 
    {
        public struct Ctx
        {
            public int lineCount;
            public int spawnTime;
            public GameObject parent;
            public ObstaclePositionFinder positionFinder;
        }

        private const int StartPosY = 210;
        private readonly Ctx _ctx;

        public CoinGenerator(Ctx ctx)
        {

            _ctx = ctx;
            Observable.Timer(System.TimeSpan.FromSeconds(_ctx.spawnTime * 2))
                .Repeat()
                .Subscribe(_ => GenerateCoins()).AddTo(_ctx.parent);
        }

        private void GenerateCoins()
        {
            float pos = _ctx.positionFinder.PossiblePosList[Random.Range(0, _ctx.positionFinder.PossiblePosList.Count)];
            for (int i = 0; i < 4; i++)
            {
                PoolManager.GetObject("Coin", new Vector3(pos, StartPosY + i*6, 0), Quaternion.identity);
            }
        }
    }
}
