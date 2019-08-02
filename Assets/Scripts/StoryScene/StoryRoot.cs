using UnityEngine;

namespace Story
{
    public class StoryRoot
    {
        public struct Ctx
        {
            public AnaliticsCore analiticsCore;
            public PlayersData playersData;
        }

        private Ctx _ctx;
        private IntroView _introView;
        public StoryRoot(Ctx ctx)
        {
            _ctx = ctx;
            //Load IntroView Prefab
            _introView = Resources.Load("Prefabs/IntroScreen") as IntroView;
        }
    }
}
