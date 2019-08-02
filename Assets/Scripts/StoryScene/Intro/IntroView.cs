﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class IntroView : MonoBehaviour
    {
        public struct Ctx
        {
            public PlayersData playersData;
        }

        [SerializeField] private Text introText;
        [SerializeField] private StoryController storyController;

        private Image _introScreen;
        private Ctx _ctx;
        void Awake()
        {
            introText.text = "";
            _introScreen = GetComponent<Image>();
        }

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }
        public void HideIntroScreen()
        {
            StartCoroutine(HideIntroByAlpha());
        }

        public IEnumerator TextPrint(string input, float delay)
        {
            //вывод текста побуквенно
            for (int i = 0; i <= input.Length; i++)
            {
                introText.text = input.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }

            Button button = GetComponent<Button>();
            _ctx.playersData.SetGameAlreadyStarted();
            button.onClick.AddListener(HideIntroScreen);
        }

        IEnumerator HideIntroByAlpha()
        {
            float nextAlpha = 1;
            while (nextAlpha > 0)
            {
                nextAlpha = _introScreen.color.a - 0.05f;
                _introScreen.color = new Color(_introScreen.color.r, _introScreen.color.g, _introScreen.color.b, nextAlpha);
                yield return null;
            }
            storyController.StartStory(); 
            gameObject.SetActive(false);
        }
    }
}