using System;

namespace Story
{
    public class Intro
    {
        public struct Ctx
        {
            public IntroView introView;
            public PlayersData playersData;
            public Action startStory;
            public Action turnOnPC;
            public bool needDelay;
        }

        private Ctx _ctx;

        public Intro(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void SetCtx(Ctx ctx)
        {
            _ctx.needDelay = ctx.needDelay;
            _ctx.turnOnPC = ctx.turnOnPC;
        }
        public void InitializeIntro()
        {
            ConfigurateView();
            if (_ctx.playersData.CheckNewGameStatus())
                _ctx.introView.StartCoroutinePrinting("Однажды, гуляя по лесу, вы набрели на необычную поляну.\nТолько стоило Вам вступить на неё, как чаща вокруг Вас превратилась в непроходимую стену из деревьев. Взгляд невольно упал на совершенно неуместную технологическую постройку по центру поляны, похожую на компьютер.\nНе зная страха, вы включили его... \n\nНажмите на экран для продолжения ", 0.01f);
            else
                _ctx.introView.HideIntroScreen();
        }
        private void ConfigurateView()
        {
            IntroView.Ctx viewCtx = new IntroView.Ctx
            {
                setIntroWatched = _ctx.playersData.SetGameAlreadyStarted,
                startStory = _ctx.startStory,
                turnOnPC = _ctx.turnOnPC,
                needDelay = _ctx.needDelay,
                SetDontNeedPcLoad = () =>_ctx.playersData.SetNeedPcLoading(0),
            };
            _ctx.introView.SetCtx(viewCtx);
        }
    }
}
