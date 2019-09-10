using UnityEngine;
using UniRx;
using System;

namespace Assets.Scripts.Score
{
    public class ScoreContainer
    {
        public struct Ctx
        {
            public IntVariable score;
            public IntVariable maxScore;
            public GameObject parent;
            public PlayersData playerData;
        }

        private readonly Ctx _ctx;
        private IDisposable _tickHandler;

        public ScoreContainer(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.score.Value = 0;
            _ctx.maxScore.Value = _ctx.playerData.GetMaxScore();
            _tickHandler = Observable.EveryFixedUpdate()
                .Subscribe(_ => Tick())
                .AddTo(_ctx.parent);
        }

        internal void Tick()
        {
            _ctx.score.Value++;
        }

        bool CheckNewRecord()
        {
            return _ctx.score.Value > _ctx.maxScore.Value;
        }

        public void OnHeroDeath()
        {
            _tickHandler?.Dispose();
            if (CheckNewRecord())
            {
                _ctx.maxScore.Value = _ctx.score.Value;
                _ctx.playerData.SetMaxScore(_ctx.maxScore.Value);
            }
        }
    }
}
