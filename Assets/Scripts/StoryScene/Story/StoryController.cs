using System.Collections;
using System.Collections.Generic;
using Cradle;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class StoryController : MonoBehaviour
    {
        private List<string> _varNameFromStory = new List<string>()
        {
            "firstOption",
            "secondOption",
            "thirdOption"
        };

        [SerializeField] private Cradle.Story story;
        [SerializeField] private List<Button> optionsButtons;
        [SerializeField] private Text storyText;

        public bool skip = false;
        private readonly List<StoryLink> _storyLinks = new List<StoryLink>();

        void Awake()
        {
            storyText.text = "";
            foreach (Button button in optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        public void StartStory()
        {
            story.OnOutput += OnOutput;
            story.OnPassageEnter += OnPassageEnter;
            foreach (Button button in optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetString("last_passage") != "")
            story.StartPassage = PlayerPrefs.GetString("last_passage");
            story.Begin();
        }

        void OnOutput(StoryOutput output)
        {
            if (output is StoryText)
            {
                if (output.Text.Trim().Equals("")) return;
                StartCoroutine(TextPrint(storyText, output.Text, 0.1f));
            }
            else if (output is LineBreak)
            {
                storyText.text += "\n";
            }
            else if (output is StoryLink link)
            {
                _storyLinks.Add(link);
            }
        }

        public void MakeChoice(int choice)
        {
            skip = false;
            _storyLinks.Clear();
            story.DoLink(optionsButtons[choice].GetComponentInChildren<Text>().text);
            UpdateHUD();
        }

        void OnPassageEnter(StoryPassage passage)
        {
            PlayerPrefs.SetString("last_passage", passage.Name);
            storyText.text = "";
            foreach (Button button in optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        void UpdateHUD()
        {

        }

        public void SkipText()
        {
            skip = true;
        }

        IEnumerator TextPrint( Text output, string input, float delay)
        {
            //вывод текста побуквенно
            for (int i = 0; i <= input.Length; i++)
            {
                if (skip) { output.text = input; break; }
                output.text = input.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }
            SetOptionButton();
        }

        private void SetOptionButton()
        {
            int i = 0;
            foreach (StoryLink link in _storyLinks)
            {
                optionsButtons[i].GetComponentInChildren<Text>().text = link.Text;
                optionsButtons[i].GetComponentsInChildren<Text>()[1].text = story.Vars[_varNameFromStory[i]];
                optionsButtons[i].gameObject.SetActive(true);
                i++;
            }
        }

        public int GetCoinsForOption(int option)
        {
            return (int)story.Vars[_varNameFromStory[option]];
        }
    }
}
