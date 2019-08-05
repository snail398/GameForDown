using Cradle;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class TwineStory
    {
        public struct Ctx
        {
            public TwineStoryView twineStoryView;
            public PlayersData playersData;
        }

        private readonly Ctx _ctx;
        private readonly List<StoryLink> _storyLinks = new List<StoryLink>();
        private readonly Cradle.Story _story;
        private readonly List<Button> _optionsButtons;
        private readonly List<string> _varNameFromStory = new List<string>()
        {
            "firstOption",
            "secondOption",
            "thirdOption"
        };

        public TwineStory(Ctx ctx)
        {
            _ctx = ctx;
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
            
            _story.OnOutput += OnOutput;
            _story.OnPassageEnter += OnPassageEnter;
            foreach (Button button in _optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
            if (_ctx.playersData.GetLastPassage() != "")
                _story.StartPassage = _ctx.playersData.GetLastPassage();
            _story.Begin();
        }

        void OnOutput(StoryOutput output)
        {
            if (output is StoryText)
            {
                if (output.Text.Trim().Equals("")) return;
                _ctx.twineStoryView.StartCoroutinePrinting(output.Text, 0.1f);
            }
            else if (output is LineBreak)
            {
                _ctx.twineStoryView.MakeLineBreak();
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
            _ctx.playersData.SetLastPassage(passage.Name);
            _ctx.twineStoryView.CleanText();
            foreach (Button button in _optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
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
