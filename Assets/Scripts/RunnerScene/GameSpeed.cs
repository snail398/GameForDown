using System;
using System.Collections;
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
        private float _tempGameSpeed;
        private Coroutine _slowDownCoroutine;
        private bool _isSlowDown;

        public void Initialize(Ctx ctx)
        {
            _ctx = ctx;
            ObstacleSpeed = _ctx.startGameSpeed;
            Observable.Timer(System.TimeSpan.FromSeconds(_ctx.speedIncreaseTime)).Repeat().Subscribe(_ => ObstacleSpeed += _ctx.speedIncreaseCount).AddTo(this);
        }

        public void SlowDownGame()
        {
            _isSlowDown = true;
            _tempGameSpeed = ObstacleSpeed;
            _slowDownCoroutine = StartCoroutine(SlowDownGameProcess());
        }

        public void ContinueGame()
        {
            if (!_isSlowDown) return;
            _isSlowDown = false;
            if (_slowDownCoroutine != null)
                StopCoroutine(_slowDownCoroutine);
            ObstacleSpeed = _tempGameSpeed;
        }

        IEnumerator SlowDownGameProcess()
        {
            while (ObstacleSpeed > 3f)
            {
                ObstacleSpeed = Mathf.Lerp(ObstacleSpeed, 1f, 0.02f);
                yield return null;
            }
        }
    }
}
