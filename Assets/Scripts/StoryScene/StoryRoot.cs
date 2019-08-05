using UnityEngine;
using System;

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
        }

        private readonly Ctx _ctx;
        private readonly StoryRootView _rootView;
        private TwineStory _twineStory;

        public StoryRoot(Ctx ctx)
        {
            _ctx = ctx;
            _rootView = GameObject.Find("StoryRoot").GetComponent<StoryRootView>();
            CreateTwineStory();
            CreateHUD();
            CreateIntro();
        }

        private void CreateIntro()
        {
            Intro.Ctx introCtx = new Intro.Ctx
            {
                introView = _rootView.IntroView,
                playersData = _ctx.playersData,
                startStory = _twineStory.StartStory,
            };
            new Intro(introCtx);
        }
        private void CreateTwineStory()
        {
            TwineStory.Ctx twineCtx = new TwineStory.Ctx
            {
                twineStoryView = _rootView.GetComponent<TwineStoryView>(),
                playersData = _ctx.playersData,
            };
            _twineStory = new TwineStory(twineCtx);
        }

        private void CreateHUD()
        {
            StoryHUD.Ctx hudCtx = new StoryHUD.Ctx
            {
                playersData = _ctx.playersData,
                storyHUDView = _rootView.GetComponent<StoryHUDView>(),
                twineStory = _twineStory,
                loadRunScene = _ctx.loadRunScene,
                reloadGame = _ctx.reloadGame,
            };
            new StoryHUD(hudCtx);
        }
    }
}
