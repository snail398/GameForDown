using System;

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
