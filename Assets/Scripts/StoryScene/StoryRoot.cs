using UnityEngine;
using System;
using UniRx;

namespace Story
{
    public class StoryRoot
    {
        public struct Ctx
        {
            public AnaliticsCore analiticsCore;
            public PlayersData playersData;
            public Action reloadGame;
            public Action loadRunScene;
            public Action<Transform> setCanvas;
        }

        private readonly Ctx _ctx;
        private StoryRootView _rootView;
        private TwineStory _twineStory;
        private StoryHUD _storyHUD;
        private Intro _intro;
        private readonly ReactiveProperty<bool> _needStartRunButton = new ReactiveProperty<bool>(false);

        public StoryRoot(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void Initialize()
        {
            _rootView = GameObject.Find("StoryRoot").GetComponent<StoryRootView>();
            _ctx.setCanvas?.Invoke(GameObject.Find("Canvas").transform);
            CreateTwineStory();
            CreateHUD();
            CreateIntro();
        }

        private void CreateIntro()
        {
            bool needDelay = true;
            if (!_ctx.playersData.CheckNeedPcLoading())
                needDelay = false;
            if (_intro == null)
            {
                Intro.Ctx introCtx = new Intro.Ctx
                {
                    introView = _rootView.IntroView,
                    playersData = _ctx.playersData,
                    startStory = _twineStory.StartStory,
                    turnOnPC = _storyHUD.TurnOnPC,
                    needDelay = needDelay,
                };
                _intro = new Intro(introCtx);
            }
            _intro.InitializeIntro();
        }
        private void CreateTwineStory()
        {
            if (_twineStory == null)
            {
                TwineStory.Ctx twineCtx = new TwineStory.Ctx
                {
                    twineStoryView = _rootView.GetComponent<TwineStoryView>(),
                    playersData = _ctx.playersData,
                    needStartRunButton = _needStartRunButton,
                };
                _twineStory = new TwineStory(twineCtx);
            }
            _twineStory.InitializeTwine();
        }

        private void CreateHUD()
        {
            if (_storyHUD == null)
            {
                StoryHUD.Ctx hudCtx = new StoryHUD.Ctx
                {
                    playersData = _ctx.playersData,
                    storyHUDView = _rootView.GetComponent<StoryHUDView>(),
                    twineStory = _twineStory,
                    loadRunScene = _ctx.loadRunScene,
                    reloadGame = _ctx.reloadGame,
                    needStartRunButton = _needStartRunButton,
                };
                _storyHUD = new StoryHUD(hudCtx);
            }
            _storyHUD.InitializeHUD();
        }
    }
}
