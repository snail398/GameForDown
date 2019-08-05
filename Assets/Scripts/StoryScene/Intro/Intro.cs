using UnityEngine;
using System;

namespace Story
{
    public class Intro : MonoBehaviour
    {
        public struct Ctx
        {
            public IntroView introView;
            public PlayersData playersData;
            public Action startStory;
        }

        private Ctx _ctx;

        public Intro(Ctx ctx)
        {
            _ctx = ctx;
            ConfigurateView();
            if (_ctx.playersData.CheckNewGameStatus())
                _ctx.introView.StartCoroutinePrinting("Вы - молодой человек, странствующий по миру в поисках приключений.\nОднажды, изучая бескрайние просторы Арктики вы набрели на заброшенную лабораторию.\nТолько стоило Вам войти внутрь, как дверь за вами наглухо захлопнулась и вы оказались в небольшой пустой комнате. Единстенная вещь - это компьютер в центре комнаты.\nНе зная страха, вы включили его... \n\nНажмите на экран для продолжения ", 0.01f);
            else
                _ctx.introView.HideIntroScreen();
        }

        private void ConfigurateView()
        {
            IntroView.Ctx viewCtx = new IntroView.Ctx
            {
                setIntroWatched = _ctx.playersData.SetGameAlreadyStarted,
                startStory = _ctx.startStory,
            };
            _ctx.introView.SetCtx(viewCtx);
        }
    }
}
