using Cradle;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine.UI;

namespace Story
{
    public class TwineStory
    {
        public struct Ctx
        {
            public TwineStoryView twineStoryView;
            public PlayersData playersData;
            public ReactiveProperty<bool> needStartRunButton;
            public AnaliticsCore analitics;
        }

        private readonly Ctx _ctx;
        private readonly List<StoryLink> _storyLinks = new List<StoryLink>();
        private Cradle.Story _story;
        private StoryPassage _printedPassage;
        private List<Button> _optionsButtons;
        private readonly List<string> _varNameFromStory = new List<string>
        {
            "firstOption",
            "secondOption",
            "thirdOption"
        };
        private readonly Dictionary<string, int> _passageWithoutRunButton = new Dictionary<string, int>
        {
            { "beemoIntroduce", 1 },
            { "beemoStartTutorial", 2 },
            { "beemoHahaMoney", 3 },
            { "beemoMoney", 4 },
        };
        private string _fullPassageText;

        public TwineStory(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void InitializeTwine()
        {
            _story = _ctx.twineStoryView.CradleStory;
            _optionsButtons = _ctx.twineStoryView.OptionsButtons;
            TwineStoryView.Ctx viewCtx = new TwineStoryView.Ctx
            {
                setOptionsButton = SetOptionButton,
            };
            _ctx.twineStoryView.SetCtx(viewCtx);
        }

        public void StartStory()
        {
            _ctx.twineStoryView.SubscribeScreenClick();
            _story.OnOutput += OnOutput;
            _story.OnPassageEnter += OnPassageEnter;
            _story.OnPassageDone += OnPassageDone;
            foreach (Button button in _optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
            string dataPassage = _ctx.playersData.GetLastPassage();
            if (dataPassage == "beemoGoToRunTutorial" && !_ctx.playersData.CheckNeedRunTutorial())
                dataPassage = "beemoTutorialEnd";
            if (dataPassage != "")
                if (TryFindPassage(dataPassage))
                    _story.StartPassage = dataPassage;
            _story.Begin();
        }

        private void OnPassageDone(StoryPassage passage)
        {
            if (_printedPassage != passage)
            {
                _printedPassage = passage;
                _ctx.twineStoryView.StartCoroutinePrinting(_fullPassageText, 0.05f);
            }
        }

        private bool TryFindPassage(string dataPassage)
        {
            return _story.Passages.TryGetValue(dataPassage, out StoryPassage outPassage);
        }

        void OnOutput(StoryOutput output)
        {
            if (output is StoryText)
            {
                if (output.Text.Trim().Equals("")) return;
                _fullPassageText += output.Text;
            }
            else if (output is LineBreak)
            {
                _fullPassageText += "\n";
            }
            else if (output is StoryLink link)
            {
                _storyLinks.Add(link);
            }
        }

        public void MakeChoice(int choice)
        {
            _ctx.twineStoryView.RefreshSkip();
            _storyLinks.Clear();
            _story.DoLink(_optionsButtons[choice].GetComponentInChildren<Text>().text);
        }
        
        public void OnPassageEnter(StoryPassage passage)
        {
            CheckSpecificPassage(passage);
            _fullPassageText = "";
            _ctx.playersData.SetLastPassage(passage.Name);
            _ctx.twineStoryView.CleanText();
            foreach (Button button in _optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        private void CheckSpecificPassage(StoryPassage passage)
        {
            if (passage.Name == "beemoGoToRunTutorial")
                _ctx.analitics.SendStoryTutorialPassed();
            /*
             * Проверка, нужно ли отображать кнопку
             * "Добыть монеты" для перехода в раннер
             * В ранних пассажах её необходимо скрывать
             */
            if ( _passageWithoutRunButton.TryGetValue(passage.Name, out _))
                _ctx.needStartRunButton?.SetValueAndForceNotify(false);
            else
                _ctx.needStartRunButton?.SetValueAndForceNotify(true);
        }

        private void SetOptionButton()
        {
            int i = 0;
            foreach (StoryLink link in _storyLinks)
            {
                _optionsButtons[i].GetComponentInChildren<Text>().text = link.Text;
                _optionsButtons[i].GetComponentsInChildren<Text>()[1].text = _story.Vars[_varNameFromStory[i]];
                _optionsButtons[i].gameObject.SetActive(true);
                i++;
            }
        }

        public int GetCoinsForOption(int option)
        {
            return (int)_story.Vars[_varNameFromStory[option]];
        }
    }
}
