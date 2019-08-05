using UnityEngine;
using UniRx;

namespace Assets.Scripts.Score
{
    public class ScoreContainer
    {
        public struct Ctx
        {
            public IntVariable score;
            public IntVariable maxScore;
            public GameObject parent;
        }

        private readonly Ctx _ctx;

        public ScoreContainer(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.score.Value = 0;
            Observable.EveryUpdate()
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
            if (CheckNewRecord()) _ctx.maxScore.Value = _ctx.score.Value;
        }
    }
}
