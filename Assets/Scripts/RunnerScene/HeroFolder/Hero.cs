﻿using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.HeroFolder
{

    public class Hero
    {
        public struct Ctx
        {
            public int lineCount;
            public int currentHealth;
            public int maxHeath;
            public HeroView view;
            public List<Color> availableColors;
            public List<Mesh> availableMeshes;
            public int lineWidth;
            public Action continueGame;
            public ReactiveProperty<int> blockSwipeCommand;
            public ReactiveCommand hideTutorialView;
            public Action restartScene;
            public Action returnToStoryScene;
            public Func<bool> isPaused;
            public Action stopRun;
        }

        private readonly Ctx _ctx;

        public Hero(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void InitializeHero()
        {
            if (_ctx.currentHealth > _ctx.maxHeath) throw new IndexOutOfRangeException("Current Health cannot be more than Max health");
            if (_ctx.currentHealth < 0 || _ctx.maxHeath < 0) throw new IndexOutOfRangeException(" Health cannot be less than zero");
            this.CurrentHealth = _ctx.currentHealth;
            this.MaxHeath = _ctx.maxHeath;

            HeroView.Ctx heroViewCtx = new HeroView.Ctx
            {
                lineCount = _ctx.lineCount,
                strafe = _ctx.lineWidth,
                lastLeftCoord = -GetLastValue(),
                lastRightCoord = GetLastValue(),
                availableColors = _ctx.availableColors,
                availableMeshes = _ctx.availableMeshes,
                continueGame = _ctx.continueGame,
                blockSwipeCommand = _ctx.blockSwipeCommand,
                hideTutorialView = _ctx.hideTutorialView,
                isPaused = _ctx.isPaused,
            };
            _ctx.view.SetCtx(heroViewCtx);
            HeroDeathView.Ctx deathViewCtx = new HeroDeathView.Ctx
            {
                restartScene = _ctx.restartScene,
                returnToStoryScene = _ctx.returnToStoryScene,
                stopRun = _ctx.stopRun,
            };
            _ctx.view.GetComponent<HeroDeathView>().SetCtx(deathViewCtx);
        }

        public int CurrentHealth { get; internal set; }

        public int MaxHeath { get; internal set; }
        
        private int GetLastValue()
        {
            return _ctx.lineCount - 1;
        }
    }
}