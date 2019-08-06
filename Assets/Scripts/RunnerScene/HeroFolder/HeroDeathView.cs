﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Assets.Scripts.HeroFolder
{
    public class HeroDeathView : MonoBehaviour
    {
        public struct Ctx
        {
            public Action restartScene;
            public Action returnToStoryScene;
        }

        private Ctx _ctx;
        private bool _isTutorial = false;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        private void RestartScene()
        {
            _ctx.restartScene?.Invoke();
        }

        public void ChangeSceneOnHeroDeath()
        {
            if (CheckIsTutorial())
            {
                RestartScene();
            }
            else
            {
                SetSceneToStory();
            }
        }

        private bool CheckIsTutorial()
        {
            return _isTutorial;
        }

        private void SetSceneToStory()
        {
            _ctx.returnToStoryScene?.Invoke();
        }

        public void InTutorial()
        {
            _isTutorial = true;
        }

        public void OutTutorial()
        {
            _isTutorial = false;
        }
    }
}
