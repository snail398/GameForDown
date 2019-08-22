using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class TwineStoryView : MonoBehaviour
    {
        public struct Ctx
        {
            public Action setOptionsButton;
        }

        [SerializeField] private Cradle.Story _story;
        [SerializeField] private List<Button> _optionsButtons;
        [SerializeField] private Text _storyText;
        [SerializeField] private Button _screenButton;

        private Ctx _ctx;

        public bool skip = false;

        public Cradle.Story CradleStory => _story;

        public List<Button> OptionsButtons => _optionsButtons;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        void Awake()
        {
            _storyText.text = "";
            foreach (Button button in _optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
               
        public void SubscribeScreenClick()
        {
            _screenButton.onClick.AddListener(SkipText);
        }

        public void RefreshSkip()
        {
            skip = false;
        }

        public void SkipText()
        {
            if (_storyText.text != "")
                skip = true;
        }

        public void StartCoroutinePrinting(string input, float delay)
        {
            StartCoroutine(TextPrint(input, delay));
        }

        public void MakeLineBreak()
        {
            _storyText.text += "\n";
        }

        public void CleanText()
        {
            _storyText.text = "";
        }

        IEnumerator TextPrint(string input, float delay)
        {
            //вывод текста побуквенно
            for (int i = 0; i <= input.Length; i++)
            {
                if (skip) { _storyText.text = input; break; }
                _storyText.text = input.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }
            _ctx.setOptionsButton?.Invoke();
        }
    }
}
