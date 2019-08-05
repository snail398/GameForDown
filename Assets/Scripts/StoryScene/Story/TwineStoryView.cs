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

        [SerializeField] private Cradle.Story story;
        [SerializeField] private List<Button> optionsButtons;
        [SerializeField] private Text storyText;

        private Ctx _ctx;

        public bool skip = false;

        public Cradle.Story CradleStory
        {
            get => story;
        }

        public List<Button> OptionsButtons
        {
            get => optionsButtons;
        }

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        void Awake()
        {
            storyText.text = "";
            foreach (Button button in optionsButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
               
        public void RefreshSkip()
        {
            skip = false;
        }
        public void SkipText()
        {
            skip = true;
        }

        public void StartCoroutinePrinting(string input, float delay)
        {
            StartCoroutine(TextPrint(input, delay));
        }

        public void MakeLineBreak()
        {
            storyText.text += "\n";
        }

        public void CleanText()
        {
            storyText.text = "";
        }

        IEnumerator TextPrint(string input, float delay)
        {
            //вывод текста побуквенно
            for (int i = 0; i <= input.Length; i++)
            {
                if (skip) { storyText.text = input; break; }
                storyText.text = input.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }
            _ctx.setOptionsButton?.Invoke();
        }
    }
}
