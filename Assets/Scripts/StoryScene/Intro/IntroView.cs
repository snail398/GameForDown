using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Story
{
    public class IntroView : MonoBehaviour
    {
        public struct Ctx
        {
            public Action setIntroWatched;
            public Action startStory;
            public Action turnOnPC;
        }

        [SerializeField] private Text introText;

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

        public void StartCoroutinePrinting(string input, float delay)
        {
            StartCoroutine(TextPrint( input, delay));
        }

        public void HideIntroScreen()
        {
            StartCoroutine(HideIntroByAlpha());
        }

         IEnumerator TextPrint(string input, float delay)
        {
            //вывод текста побуквенно
            for (int i = 0; i <= input.Length; i++)
            {
                introText.text = input.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }

            Button button = GetComponent<Button>();
            _ctx.setIntroWatched?.Invoke();
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
            _ctx.turnOnPC?.Invoke();
            Observable.Timer(TimeSpan.FromSeconds(4)).Subscribe(_ =>_ctx.startStory?.Invoke()).AddTo(this);
            gameObject.SetActive(false);
        }
    }
}
