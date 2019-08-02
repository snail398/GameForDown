using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Story
{
    public class Intro : MonoBehaviour
    {
        public struct Ctx
        {
            public IntroView introView;
            public PlayersData playersData;
        }

        private Ctx _ctx;

        public Intro(Ctx ctx)
        {
            _ctx = ctx;
            ConfigurateView();
            if (_ctx.playersData.CheckNewGameStatus())
                StartCoroutine(_ctx.introView.TextPrint("Вы - молодой человек, странствующий по миру в поисках приключений.\nОднажды, изучая бескрайние просторы Арктики вы набрели на заброшенную лабораторию.\nТолько стоило Вам войти внутрь, как дверь за вами наглухо захлопнулась и вы оказались в небольшой пустой комнате. Единстенная вещь - это компьютер в центре комнаты.\nНе зная страха, вы включили его... \n\nНажмите на экран для продолжения ", 0.1f));
            else
                _ctx.introView.HideIntroScreen();
        }

        private void ConfigurateView()
        {
            IntroView.Ctx viewCtx = new IntroView.Ctx
            {
                playersData = _ctx.playersData,
            };
            _ctx.introView.SetCtx(viewCtx);
        }
    }
}
