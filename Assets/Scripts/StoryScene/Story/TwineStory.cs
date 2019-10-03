using Cradle;
using Newtonsoft.Json;
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
        private const string DEATH_PASSAGE = "deathPassage";
        private string _toSavePassageName;
        private PassageHistory _passageHistory;

        public event Action ShowRollbackWindow;

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
            PassageHistory.Ctx passageHistoryCtx = new PassageHistory.Ctx
            {
                playersData = _ctx.playersData,
            };
            _passageHistory = new PassageHistory(passageHistoryCtx);
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
            string dataPassage = _passageHistory.GetLastPassage();
            if (TryFindPassage(dataPassage))
                _story.StartPassage = dataPassage;
            if (_passageHistory.StoryVars != null)
                SetVarsFromDict(_passageHistory.StoryVars);
            _story.Begin();
        }

        private Dictionary<string,string> UpdateVarsDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var item in _story.Vars)
            {
                string val = item.Value.InnerValue == null ? "null" : item.Value.InnerValue.ToString();
                dict.Add(item.Key,val);
            }
            return dict;
        }

        private void SetVarsFromDict(Dictionary<string, string> dict)
        {
            foreach (var item in dict)
            {
                if (item.Value.ToLower() == "true")
                    _story.Vars.SetMember(item.Key, true);
                else if (item.Value.ToLower() == "false")
                    _story.Vars.SetMember(item.Key, false);
                else
                    _story.Vars.SetMember(item.Key, item.Value);
            }
        }

        private void OnPassageDone(StoryPassage passage)
        {
            if (_printedPassage != passage)
            {
                _printedPassage = passage;
                _ctx.twineStoryView.StartCoroutinePrinting(_fullPassageText, 0.05f);
                _passageHistory.StoryVars = UpdateVarsDictionary();
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
            if (_story.Vars[DEATH_PASSAGE] == true)
            {
                _toSavePassageName = _optionsButtons[choice].GetComponentInChildren<Text>().text;
                ShowRollbackWindow?.Invoke();
            }
            else
            {
                _ctx.twineStoryView.RefreshSkip();
                _storyLinks.Clear();
                _story.DoLink(_optionsButtons[choice].GetComponentInChildren<Text>().text);
            }
        }

        public void RollBack()
        {
            _ctx.twineStoryView.RefreshSkip();
            _storyLinks.Clear();
            _story.Vars[DEATH_PASSAGE] = false;
            if (_passageHistory.TryGetPreviousPassage(out string name))
                _story.GoTo(name);
        }

        public void ToSave()
        {
            _ctx.twineStoryView.RefreshSkip();
            _storyLinks.Clear();
            _story.Vars[DEATH_PASSAGE] = false;
            _story.DoLink(_toSavePassageName);
        }
        
        public void OnPassageEnter(StoryPassage passage)
        {
            _ctx.analitics.SendCurrentPassage(passage.Name);
            CheckSpecificPassage(passage);
            _fullPassageText = "";
            _passageHistory.AddToHistory(passage.Name);
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
