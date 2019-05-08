using System;
using UniRx;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSpeed : MonoBehaviour
    {
        public struct Ctx
        {
            public float speedIncreaseTime;
            public float speedIncreaseCount;
            public float startGameSpeed;
        }
        public float ObstacleSpeed { get; set; }

        private Ctx _ctx;

        public void Initialize(Ctx ctx)
        {
            _ctx = ctx;
            ObstacleSpeed = _ctx.startGameSpeed;
            Observable.Timer(System.TimeSpan.FromSeconds(_ctx.speedIncreaseTime)).Repeat().Subscribe(_ => ObstacleSpeed += _ctx.speedIncreaseCount).AddTo(this);
        }
    }
}
