using System;
using UniRx;

namespace Story
{
    public class StoryHUD
    {
        public struct Ctx
        {
            public StoryHUDView storyHUDView;
            public PlayersData playersData;
            public TwineStory twineStory;
            public Action reloadGame;
            public Action loadRunScene;
            public ReactiveProperty<bool> needStartRunButton;
        }

        private Ctx _ctx;

        public StoryHUD(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void InitializeHUD()
        {
            StoryHUDView.Ctx viewCtx = new StoryHUDView.Ctx
            {
                playersData = _ctx.playersData,
                twineStory = _ctx.twineStory,
                loadRunScene = _ctx.loadRunScene,
                reloadGame = _ctx.reloadGame,
                needStartRunButton = _ctx.needStartRunButton,
            };
            _ctx.storyHUDView.SetCtx(viewCtx);
            if (!_ctx.playersData.CheckNeedPcLoading())
                _ctx.storyHUDView.SetPcOn();
        }

        public void TurnOnPC()
        {
            _ctx.storyHUDView.TurnOnPc();
        }
    }
}
